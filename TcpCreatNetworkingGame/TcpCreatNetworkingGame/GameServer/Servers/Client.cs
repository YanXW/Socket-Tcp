using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Common;
using MySql.Data.MySqlClient;
using GameServer.Tools;

namespace GameServer.Servers
{
    class Client
    {
        private Socket clientSocket; 
        private Server server;  //server端的引用
        private Message message = new Message(); //Message的引用，用于获取Data
        private MySqlConnection mysqlConn;

        public Client() { }
        public Client(Socket clientSocket,Server  server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            mysqlConn = ConnHelper.Connect();//建立数据集连接
        }
        public void Start()
        {
            clientSocket.BeginReceive(message.Data,message.StartIndex,message.RemainSize,SocketFlags.None,ReceCallBack,null);
        }

        private void ReceCallBack(IAsyncResult ar)
        {
            try
            {
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    CloseSocket();
                }
                message.ReadMessage(count,OnProcessMessage);
                Start();//处理完数据后重新接收
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                CloseSocket();
            }        
        }

        private void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            //将Client解析后的消息发送给中介Server处理，再由Server发送给ControllerManager进行处理
            server.HandleRequest(requestCode, actionCode, data, this);
        }

        private void CloseSocket() //关闭Client 并移除Server端的引用
        {
            ConnHelper.CloseConnection(mysqlConn);//关闭数据库连接
            if (clientSocket != null)
            {
                clientSocket.Close();
            }
            server.RemoveClient(this);
            
        }
        public void SendMessage(RequestCode requestCode, string data)
        {
            byte[] bytes = Message.PackMessage(requestCode,data);
            clientSocket.Send(bytes);
        }
    }
   

}
