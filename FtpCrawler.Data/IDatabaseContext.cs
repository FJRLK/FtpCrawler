using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace FtpCrawler.Data
{
    public interface IDatabaseContext
    {
        bool DatabaseExists();

        void ExecuteSql(String sql);

        Int32 SaveChanges();

        IDbSet<T> Set<T>() where T : BaseEntity;
    }
}
