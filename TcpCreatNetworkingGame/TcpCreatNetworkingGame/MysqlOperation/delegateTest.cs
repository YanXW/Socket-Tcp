using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MysqlOperation
{
    class delegateTest
    {
        public delegate void GreetingDelegate(string name);
        private static void EnglishGreeting(string name)
        {
            Console.WriteLine("Morning, " + name);
        }

        private static void ChineseGreeting(string name)
        {
            Console.WriteLine("早上好, " + name);
        }

        //注意此方法，它接受一个GreetingDelegate类型的方法作为参数
        private static void GreetPeople(string name, GreetingDelegate MakeGreeting)
        {
            MakeGreeting(name);
        }

        static void Main(string[] args)
        {
            //GreetPeople("Jimmy Zhang", EnglishGreeting);  //1
            //GreetPeople("张子阳", ChineseGreeting);
            //Console.ReadKey();


            //GreetingDelegate delegate1, delegate2;        //2
            //delegate1 = EnglishGreeting;
            //delegate2 = ChineseGreeting;
            //GreetPeople("Jimmy Zhang", delegate1);
            //GreetPeople("张子阳", delegate2);
            //Console.ReadKey();

              
            //GreetingDelegate delegate1;                     //3
            //delegate1 = EnglishGreeting; // 先给委托类型的变量赋值
            //delegate1 += ChineseGreeting;   // 给此委托变量再绑定一个方法
            //// 将先后调用 EnglishGreeting 与 ChineseGreeting 方法
            //GreetPeople("Jimmy Zhang", delegate1);
            //Console.ReadKey();


            GreetingDelegate delegate1 = new GreetingDelegate(EnglishGreeting);  //4
            delegate1 += ChineseGreeting;   // 给此委托变量再绑定一个方法
            // 将先后调用 EnglishGreeting 与 ChineseGreeting 方法
            GreetPeople("Jimmy Zhang", delegate1);
            Console.WriteLine();
            delegate1 -= EnglishGreeting; //取消对EnglishGreeting方法的绑定
                                       // 将仅调用 ChineseGreeting 
            GreetPeople("张子阳", delegate1);
            Console.ReadKey();
        }
    }
}
