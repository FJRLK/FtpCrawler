using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace FtpCrawler.Data
{
    public class DatabaseConfiguration : DbConfiguration
    {
        public DatabaseConfiguration()
        {
            SetHistoryContext("MySql.Data.MySqlClient", (conn, schema) => new FMHistoryContext(conn, schema));
        }
    }
}
