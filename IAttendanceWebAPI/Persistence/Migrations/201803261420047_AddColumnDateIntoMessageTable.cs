using System.Data.Entity.Migrations;

namespace IAttendanceWebAPI.Migrations
{
    public partial class AddColumnDateIntoMessageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Date", c => c.DateTime(false));
        }

        public override void Down()
        {
            DropColumn("dbo.Messages", "Date");
        }
    }
}