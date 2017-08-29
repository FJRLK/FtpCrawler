namespace FtpCrawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usersettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        Key = c.String(nullable: false, maxLength: 250, storeType: "nvarchar"),
                        Type = c.String(nullable: false, maxLength: 2500, storeType: "nvarchar"),
                        Value = c.String(nullable: false, maxLength: 2500, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebUser",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        UserName = c.String(nullable: false, maxLength: 250, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 2500, storeType: "nvarchar"),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WebUser");
            DropTable("dbo.Setting");
        }
    }
}
