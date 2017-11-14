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
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp); //create a socket of server
            //IPAddress iPAddress = new IPAddress(new byte[] {127,0,0,1});
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress,88);

            serverSocket.Bind(iPEndPoint);//bind the ip and port

            serverSocket.Listen(0);
            Socket clientSocket= serverSocket.Accept();

            string Msg = "hello ,client 你好...";
            byte[] sendData = System.Text.Encoding.UTF8.GetBytes(Msg);
            clientSocket.Send(sendData);

            byte[] dataBuff = new byte[512];
            int count = clientSocket.Receive(dataBuff);
            string msgRecv = System.Text.Encoding.UTF8.GetString(dataBuff,0,count);
            Console.Write(msgRecv);

            Console.ReadKey();
            clientSocket.Close();
            serverSocket.Close();



        }
    }
}
