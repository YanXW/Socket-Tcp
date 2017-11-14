﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace TcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"),88));

            byte[] data = new byte[512];
            int count = clientSocket.Receive(data);
            string msg = Encoding.UTF8.GetString(data,0,count);
            Console.WriteLine(msg);

            string s = Console.ReadLine();
            clientSocket.Send(Encoding.UTF8.GetBytes(s));

            Console.ReadKey();

            clientSocket.Close(); 
           
        }
    }
}