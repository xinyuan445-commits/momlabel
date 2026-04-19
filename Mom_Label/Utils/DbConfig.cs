using System;
using System.Configuration;

namespace Mom_Label.Utils
{
    public static class DbConfig
    {
        /// <summary>
        /// 获取数据库类型 (如: SqlServer, MySql, Oracle)
        /// </summary>
        public static string ConnectionType
        {
            get
            {
                return ConfigurationManager.AppSettings["DbConnectionType"] ?? "SqlServer";
            }
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["DbConnectionString"] ?? string.Empty;
            }
        }
    }
}