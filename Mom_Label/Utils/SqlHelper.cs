using System;
using System.Data;
using System.Data.SqlClient;

namespace Mom_Label.Utils
{
    public class SqlHelper
    {
        /// <summary>
        /// 执行查询，返回 DataTable
        /// </summary>
        public static DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        try
                        {
                            adapter.Fill(dt);
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"数据库查询异常: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}