using System.Data.Entity.Migrations;

namespace IAttendanceWebAPI.Migrations
{
    public partial class AddRecognitionImageEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.RecognitionImages",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Uri = c.String(false),
                        SecureUri = c.String(false),
                        TimeTableId = c.Int(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TimeTables", t => t.TimeTableId)
                .Index(t => t.Id)
                .Index(t => t.TimeTableId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.RecognitionImages", "TimeTableId", "dbo.TimeTables");
            DropIndex("dbo.RecognitionImages", new[] {"TimeTableId"});
            DropIndex("dbo.RecognitionImages", new[] {"Id"});
            DropTable("dbo.RecognitionImages");
        }
    }
}