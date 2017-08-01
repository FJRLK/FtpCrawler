using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace FtpCrawler.Data
{
    public class DefaultDatabaseInitializer : MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>
    {

        


        ///// <summary>
        ///// Initialize the database
        ///// </summary>
        //public override void InitializeDatabase(DatabaseContext context)
        //{
            


        //    //Create a new database if it doesn't already exist
        //    if (!context.Database.Exists())
        //    {
        //        //Create the database
        //        context.Database.Create();
        //    }

        //    DbMigrator migrator = new DbMigrator(new Migrations.Configuration());
        //    migrator.Update();

        //    //if( !context.Database.CompatibleWithModel( false ) ) return;
        //}
    }
}
