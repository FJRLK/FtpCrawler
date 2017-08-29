namespace FtpCrawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateServerFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FtpServer", "LastOnline", c => c.DateTime(precision: 0));
            AddColumn("dbo.FtpServer", "TotalFileSize", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FtpServer", "TotalFileSize");
            DropColumn("dbo.FtpServer", "LastOnline");
        }
    }
}
