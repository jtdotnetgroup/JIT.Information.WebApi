using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MySqlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string constr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            MySqlConnection conn;

            using (conn=new MySqlConnection(constr))
            {
                conn.Open();
                Console.WriteLine("ok");
                Console.ReadKey();
            }
        }
    }
}
