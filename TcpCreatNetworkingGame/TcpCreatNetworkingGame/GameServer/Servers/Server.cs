using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using GameServer.Controller;
using Common;


namespace GameServer.Servers
{
    class Server
    {
        private Socket serverSocket;
        private IPEndPoint  iPEndPoint; //IP
        private List<Client> clientList ; //连接的Client列表
        private  ControllerManager controllerManager; //持有ControllerManager的引用
         
        public  Server() { }
        public Server(String ipStr,int port)
        {
            controllerManager = new ControllerManager(this);
            SetIPAndPort(ipStr,port);
        }
        private void SetIPAndPort(String ipStr, int port) //设置IP和Port
        {
            iPEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }

        public void Start() //启动服务器
        {
            serverSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.IPv4);
            serverSocket.Bind(iPEndPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack,null);
        }
        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);  //创建一个客户端获得服务器端的连接
            Client client = new Client(clientSocket,this);  //创建一个Client的引用
            client.Start();
            clientList.Add(client);
        }
        public void RemoveClient(Client client)
        {
            lock (clientList)
            {
                clientList.Remove(client);
            }
            
        }

        /// <summary>
        /// 由Controller控制客服端向Client发送响应，减低耦合性
        /// </summary>
        /// <param name="client">目标客户端</param>
        /// <param name="requestCode">requestCode</param>
        /// <param name="data">发送的数据</param>
        public void SendResponse( Client client,RequestCode requestCode ,string  data) 
        {
            client.SendMessage(requestCode,data); //由server来进行与client交互发送
        }
       public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            //将client的响应请求发送给ControllerManager
            controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }
    }
}
