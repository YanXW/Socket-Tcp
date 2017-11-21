using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MysqlOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            string myStr = "Database=battle;Data Source=127.0.0.1;port = 3306;User ID =root;Password=201526703032";
            MySqlConnection conn = new MySqlConnection(myStr);

            conn.Open();
            #region //query
            MySqlCommand cmd = new MySqlCommand("Select * from user", conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String userName = reader.GetString("username");
                String userId = reader.GetString("userId");
                Console.WriteLine(userName + userId);

            }
            reader.Close();
            #endregion
            conn.Close();
            Console.ReadKey();
            { }
        }
    }
}
