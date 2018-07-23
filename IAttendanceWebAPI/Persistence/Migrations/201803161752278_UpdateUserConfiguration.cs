using System.Data.Entity.Migrations;

namespace IAttendanceWebAPI.Migrations
{
    public partial class UpdateUserConfiguration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(maxLength: 100, nullable: true));
            AlterColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime(true));
        }

        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime(true));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(true, 100));
        }
    }
}