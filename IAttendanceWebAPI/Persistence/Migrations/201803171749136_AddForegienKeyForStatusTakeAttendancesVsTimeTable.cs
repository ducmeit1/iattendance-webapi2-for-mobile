using System.Data.Entity.Migrations;

namespace IAttendanceWebAPI.Migrations
{
    public partial class AddForegienKeyForStatusTakeAttendancesVsTimeTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO StatusTakeAttendances(Id, Name) VALUES (1, 'Waiting')");
            Sql("INSERT INTO StatusTakeAttendances(Id, Name) VALUES (2, 'Taken')");
            Sql("UPDATE TimeTables SET StatusTakeAttendanceId = 1 WHERE Id IN (1, 2, 3, 4)");
            AddForeignKey("dbo.TimeTables", "StatusTakeAttendanceId", "dbo.StatusTakeAttendances", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.TimeTables", "StatusTakeAttendanceId", "dbo.StatusTakeAttendances");
        }
    }
}