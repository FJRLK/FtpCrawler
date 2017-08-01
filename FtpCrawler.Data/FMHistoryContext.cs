using System.Data.Common;
using System.Data.Entity;
using System;
using System.Data.Entity.Migrations.History;

namespace FtpCrawler.Data
{
    public class FMHistoryContext : HistoryContext
    {
        public FMHistoryContext(DbConnection existingConnection, String defaultSchema)
            : base(existingConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoryRow>().Property(h => h.MigrationId).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<HistoryRow>().Property(h => h.ContextKey).HasMaxLength(200).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
