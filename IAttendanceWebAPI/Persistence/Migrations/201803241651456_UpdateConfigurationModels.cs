using System.Data.Entity.Migrations;

namespace IAttendanceWebAPI.Migrations
{
    public partial class UpdateConfigurationModels : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.StudentsInGroup", "StudentId", "__mig_tmp__0");
            RenameColumn("dbo.StudentsInGroup", "StudentGroupId", "StudentId");
            RenameColumn("dbo.StudentsInGroup", "__mig_tmp__0", "StudentGroupId");
            RenameIndex("dbo.StudentsInGroup", "IX_StudentGroupId", "__mig_tmp__0");
            RenameIndex("dbo.StudentsInGroup", "IX_StudentId", "IX_StudentGroupId");
            RenameIndex("dbo.StudentsInGroup", "__mig_tmp__0", "IX_StudentId");
        }

        public override void Down()
        {
            RenameIndex("dbo.StudentsInGroup", "IX_StudentId", "__mig_tmp__0");
            RenameIndex("dbo.StudentsInGroup", "IX_StudentGroupId", "IX_StudentId");
            RenameIndex("dbo.StudentsInGroup", "__mig_tmp__0", "IX_StudentGroupId");
            RenameColumn("dbo.StudentsInGroup", "StudentGroupId", "__mig_tmp__0");
            RenameColumn("dbo.StudentsInGroup", "StudentId", "StudentGroupId");
            RenameColumn("dbo.StudentsInGroup", "__mig_tmp__0", "StudentId");
        }
    }
}