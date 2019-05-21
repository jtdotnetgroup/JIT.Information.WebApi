using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CommonTools
{
    public class SQLStoredProcedureHelper
    {
        /// <summary>
        /// 执行存储过程，返回DataTable对象
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="inputParams">传入参数</param>
        /// <returns></returns>
        public static DataTable Exec(string spName, string connStr, Dictionary<string, object> inputParams)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(spName, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var key in inputParams.Keys)
                    {
                        cmd.Parameters.Add(new SqlParameter(key, inputParams[key]));
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable table = new DataTable();


                    da.Fill(table);

                    connection.Close();

                    return table;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}