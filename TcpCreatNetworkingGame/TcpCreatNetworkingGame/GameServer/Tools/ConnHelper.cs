using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GameServer.Tools
{

    /// <summary>
    /// 与数据库连接的操作都在这里
    /// </summary>
    class ConnHelper
    {
        public const string CONNECTIONGSTRING = "dataSource =127.0.0.1;port = 3306;database=battle;user=root; pwd=root";

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection Connect()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(CONNECTIONGSTRING);
                connection.Open();
                return connection;
            }
            catch (Exception e)
            {
                throw;
                Console.WriteLine("连接数据库出现错误：" + e);
                return null;
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="connection">目标数据库</param>
        public static void CloseConnection(MySqlConnection connection)
        {
            if (connection != null)
            {
                connection.Close();
            }
            Console.WriteLine("MysqlConnection 不能为空");
        }
    }
}
