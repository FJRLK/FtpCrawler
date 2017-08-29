using System;
using System.Data.Entity.Migrations;

namespace FtpCrawler.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "FtpCrawler";
        }

        protected override void Seed(DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Set<Models.Setting>().AddOrUpdate(
                new Models.Setting { Key = "RemoveServerAfter", Type = typeof(Int32).FullName, Value = "10" },
                new Models.Setting { Key = "RemoveFileFolderAfter", Type = typeof(Int32).FullName, Value = "1" }
                );
        }
    }
}