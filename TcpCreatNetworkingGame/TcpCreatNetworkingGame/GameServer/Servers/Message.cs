using System;
using Common;
using System.Text;
using System.Linq;

namespace GameServer.Servers
{

    /// <summary>
    /// 消息与数据处理的类
    /// </summary>
    class Message
    {
        private byte[] data = new byte[1024]; 
        private int startIndex = 0; //数据索引序号

        public byte[] Data
        {
            get { return data; }
        }
        public int StartIndex
        {
            get { return startIndex; }
        } 
        public int RemainSize //数据剩余长度 
        {
            get { return data.Length - startIndex; }
        }
        //public void addCount(int count) //更新Count
        //{
        //    startIndex += count; //此方法融合到下面readMessage里边了
        //}

        /// <summary>
        /// 数据的解析
        /// </summary>
        /// <param name="newDateAmount">数据长度</param>
        /// <param name="ProcessDataCallBack">回调事件</param>
        public void ReadMessage(int newDateAmount, Action<RequestCode, ActionCode, String> ProcessDataCallBack)
        {
            startIndex += newDateAmount;
            while (true)
            {
                if (startIndex <= 4) return;
                int count = BitConverter.ToInt32(data, 0); //解析数据长度
                if ((startIndex - count) > 4)
                {
                    //Console.WriteLine(count);
                    //Console.WriteLine(startIndex);
                    //string s = Encoding.UTF8.GetString(data, 4, count);
                    //Console.WriteLine("解析出一条数据:" + s);

                    //数据=数据长度（4）+requestCode（4）+actionCode（4）+ 数据
                    RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4); //解析requestcode
                    ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8); //解析ActionCode
                    string s = Encoding.UTF8.GetString(data, 12, count-8); //解析数据

                    ProcessDataCallBack(requestCode,actionCode,s);

                    Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                    startIndex -= (4 + count);
                }
                else
                {
                    break;
                }
            }

        }


        /// <summary>
        /// 作用：将数据包装 
        /// 返回给Client的数据= 数据长度 + requestCode + data
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="data"></param>
        public static byte[] PackMessage(RequestCode requestCode, string data)
        {
            byte[] requsetCodeBytes = BitConverter.GetBytes((int)requestCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataAmount = requsetCodeBytes.Length + dataBytes.Length;
            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
            return dataAmountBytes.Concat(requsetCodeBytes).Concat(dataBytes); //将两个数组组拼成一条数据去发送
        }
    }
}
