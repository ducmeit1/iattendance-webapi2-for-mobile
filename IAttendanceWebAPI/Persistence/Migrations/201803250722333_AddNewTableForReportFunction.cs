using System.Data.Entity.Migrations;

namespace IAttendanceWebAPI.Migrations
{
    public partial class AddNewTableForReportFunction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Messages",
                    c => new
                    {
                        Id = c.Int(false, true),
                        FromUserId = c.String(false, 128),
                        ToUserId = c.String(false, 128),
                        Topic = c.String(false, 250),
                        Description = c.String(false),
                        StatusMessageId = c.Int(false),
                        TimeTableId = c.Int(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StatusMessages", t => t.StatusMessageId)
                .ForeignKey("dbo.TimeTables", t => t.TimeTableId)
                .ForeignKey("dbo.AspNetUsers", t => t.ToUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.FromUserId)
                .Index(t => t.Id)
                .Index(t => t.FromUserId)
                .Index(t => t.ToUserId)
                .Index(t => t.StatusMessageId)
                .Index(t => t.TimeTableId);

            CreateTable(
                    "dbo.StatusMessages",
                    c => new
                    {
                        Id = c.Int(false),
                        Name = c.String(false, 50)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Messages", "FromUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ToUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "TimeTableId", "dbo.TimeTables");
            DropForeignKey("dbo.Messages", "StatusMessageId", "dbo.StatusMessages");
            DropIndex("dbo.StatusMessages", new[] {"Id"});
            DropIndex("dbo.Messages", new[] {"TimeTableId"});
            DropIndex("dbo.Messages", new[] {"StatusMessageId"});
            DropIndex("dbo.Messages", new[] {"ToUserId"});
            DropIndex("dbo.Messages", new[] {"FromUserId"});
            DropIndex("dbo.Messages", new[] {"Id"});
            DropTable("dbo.StatusMessages");
            DropTable("dbo.Messages");
        }
    }
}