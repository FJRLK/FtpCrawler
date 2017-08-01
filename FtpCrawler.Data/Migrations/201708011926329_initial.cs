namespace FtpCrawler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FtpFolder",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ServerId = c.Long(nullable: false),
                        FolderId = c.Long(),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ShortName = c.String(nullable: false, maxLength: 250, unicode: false, storeType: "nvarchar"),
                        FullName = c.String(nullable: false, maxLength: 2500, unicode: false, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FtpFolder", t => t.FolderId)
                .ForeignKey("dbo.FtpServer", t => t.ServerId, cascadeDelete: true)
                .Index(t => t.FolderId)
                .Index(t => t.ServerId);
            
            CreateTable(
                "dbo.FtpFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ServerId = c.Long(nullable: false),
                        FolderId = c.Long(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        ShortName = c.String(nullable: false, maxLength: 250, unicode: false, storeType: "nvarchar"),
                        FullName = c.String(nullable: false, maxLength: 2500, unicode: false, storeType: "nvarchar"),
                        Extension = c.String(nullable: false, maxLength: 10, unicode: false, storeType: "nvarchar"),
                        FileSize = c.Long(nullable: false),
                        FileDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FtpFolder", t => t.FolderId, cascadeDelete: true)
                .ForeignKey("dbo.FtpServer", t => t.ServerId, cascadeDelete: true)
                .Index(t => t.FolderId)
                .Index(t => t.ServerId);
            
            CreateTable(
                "dbo.FtpServer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Modified = c.DateTime(nullable: false, precision: 0),
                        HostName = c.String(nullable: false, maxLength: 250, unicode: false, storeType: "nvarchar"),
                        Login = c.String(nullable: false, maxLength: 250, unicode: false, storeType: "nvarchar"),
                        PassWord = c.String(nullable: false, maxLength: 250, unicode: false, storeType: "nvarchar"),
                        Port = c.Int(nullable: false),
                        StartingDir = c.String(nullable: false, maxLength: 250, unicode: false, storeType: "nvarchar"),
                        FileList = c.String(nullable: false, maxLength: 250, unicode: false, storeType: "nvarchar"),
                        Comment = c.String(nullable: false, maxLength: 250, unicode: false, storeType: "nvarchar"),
                        EditableBy = c.String(nullable: false, maxLength: 250, unicode: false, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FtpFolder", "ServerId", "dbo.FtpServer");
            DropForeignKey("dbo.FtpFolder", "FolderId", "dbo.FtpFolder");
            DropForeignKey("dbo.FtpFile", "ServerId", "dbo.FtpServer");
            DropForeignKey("dbo.FtpFile", "FolderId", "dbo.FtpFolder");
            DropIndex("dbo.FtpFolder", new[] { "ServerId" });
            DropIndex("dbo.FtpFolder", new[] { "FolderId" });
            DropIndex("dbo.FtpFile", new[] { "ServerId" });
            DropIndex("dbo.FtpFile", new[] { "FolderId" });
            DropTable("dbo.FtpServer");
            DropTable("dbo.FtpFile");
            DropTable("dbo.FtpFolder");
        }
    }
}
