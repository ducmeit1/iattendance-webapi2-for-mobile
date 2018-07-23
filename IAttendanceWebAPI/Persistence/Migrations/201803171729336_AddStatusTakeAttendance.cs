using System.Data.Entity.Migrations;

namespace IAttendanceWebAPI.Migrations
{
    public partial class AddStatusTakeAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.StatusTakeAttendances",
                    c => new
                    {
                        Id = c.Int(false),
                        Name = c.String(false, 100)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            AddColumn("dbo.TimeTables", "StatusTakeAttendanceId", c => c.Int(false));
            CreateIndex("dbo.TimeTables", "StatusTakeAttendanceId");
        }

        public override void Down()
        {
            DropIndex("dbo.StatusTakeAttendances", new[] {"Id"});
            DropIndex("dbo.TimeTables", new[] {"StatusTakeAttendanceId"});
            DropColumn("dbo.TimeTables", "StatusTakeAttendanceId");
            DropTable("dbo.StatusTakeAttendances");
        }
    }
}