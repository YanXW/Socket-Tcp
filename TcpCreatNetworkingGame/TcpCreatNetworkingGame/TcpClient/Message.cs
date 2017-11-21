using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpClient
{
    class Message
    {
        //将数据变为 数据长度+数据
        public static byte[] GetBytes(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);//将string数据装为byte
            int dataLength = dataBytes.Length;  //获取数据长度
            byte[] lengthBytes = BitConverter.GetBytes(dataLength); // 获取字节长度
            byte[] newBytes=lengthBytes.Concat(dataBytes).ToArray(); // 连接两个字符串
            return newBytes;
        }
    }
}
