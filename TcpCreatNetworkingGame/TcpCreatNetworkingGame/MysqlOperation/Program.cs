//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MySql.Data.MySqlClient;

//namespace MysqlOperation
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string connstr = "Database=battle;Data Source=127.0.0.1;port = 3306;User ID =root;Password=201526703032";
//            MySqlConnection conn = new MySqlConnection(connstr);

//            conn.Open();

//            #region Updata
//            MySqlCommand cmd = new MySqlCommand("update user set passWord=@psw where userId=3", conn);
//            cmd.Parameters.AddWithValue("psw", 04);
//            cmd.ExecuteNonQuery();
//            #endregion

//            #region del
//            MySqlCommand cmd = new MySqlCommand("delet from user where userId=@Id", conn);
//            cmd.Parameters.AddWithValue("Id", 3);
//            cmd.ExecuteNonQuery();

//            #endregion

//            #region Insert ，addwithvalue方式防止Sql注入
//            string userName = "LN", passWord = "03";
//            MySqlCommand cmd = new MySqlCommand("insert into user set userId =@id,userName=@un , passWord=@pwd", conn);
//            cmd.Parameters.AddWithValue("un", userName);
//            cmd.Parameters.AddWithValue("pwd", passWord);
//            cmd.Parameters.AddWithValue("id", 3);
//            cmd.ExecuteNonQuery(); //执行非查询语句

//            #endregion

//            #region query
//            MySqlCommand cmd = new MySqlCommand("Select * from user", conn);

//            MySqlDataReader reader = cmd.ExecuteReader();

//            while (reader.Read())
//            {
//                String userName = reader.GetString("username");
//                String userId = reader.GetString("userId");
//                Console.WriteLine(userName + userId);

//            }
//            reader.Close();
//            #endregion
//            conn.Close();
//            Console.ReadKey();
//            { }
//        }
//    }
//}
