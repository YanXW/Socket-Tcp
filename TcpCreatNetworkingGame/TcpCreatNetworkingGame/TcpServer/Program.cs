using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServerAsync();
            Console.ReadKey();
        }

        static byte[] dataBuff = new byte[1024];
        static void StartServerAsync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //create a socket of server
            //IPAddress iPAddress = new IPAddress(new byte[] {127,0,0,1});
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 88);

            serverSocket.Bind(iPEndPoint);//bind the ip and port

            serverSocket.Listen(0);
            //Socket clientSocket = serverSocket.Accept();
            serverSocket.BeginAccept(AcceptCallBack,serverSocket);
            
        }
        static void AcceptCallBack(IAsyncResult ar)
        {
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket = serverSocket.EndAccept(ar);
            string Msg = "hello ,client 你好...";
            byte[] sendData = System.Text.Encoding.UTF8.GetBytes(Msg);
            clientSocket.Send(sendData);
            //异步接受数据
            clientSocket.BeginReceive(dataBuff, 0, 1024, SocketFlags.None, ReceCallBack, clientSocket);

            serverSocket.BeginAccept(AcceptCallBack, serverSocket); //循环调用等待接收
        }
        static void ReceCallBack(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar);//结束异步接受数据
                if (count == 0)
                {
                    clientSocket.Close();return;
                }
                string msg = Encoding.UTF8.GetString(dataBuff, 0, count);
                Console.WriteLine("接受客户端的消息：" + msg);
                clientSocket.BeginReceive(dataBuff, 0, 1024, SocketFlags.None, ReceCallBack, clientSocket);//回调，循环
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                if(clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
            finally
            {
            }
            
        }
        void StartServerSync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //create a socket of server
            //IPAddress iPAddress = new IPAddress(new byte[] {127,0,0,1});
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 88);

            serverSocket.Bind(iPEndPoint);//bind the ip and port

            serverSocket.Listen(0);
            Socket clientSocket = serverSocket.Accept();

            string Msg = "hello ,client 你好...";
            byte[] sendData = System.Text.Encoding.UTF8.GetBytes(Msg);
            clientSocket.Send(sendData);

            byte[] dataBuff = new byte[512];
            int count = clientSocket.Receive(dataBuff);
            string msgRecv = System.Text.Encoding.UTF8.GetString(dataBuff, 0, count);
            Console.Write(msgRecv);

            Console.ReadKey();
            clientSocket.Close();
            serverSocket.Close();
        }
    }
}
