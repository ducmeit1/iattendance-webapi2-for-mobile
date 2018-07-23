using System.Data.Entity.Migrations;

namespace IAttendanceWebAPI.Migrations
{
    public partial class ChangeRelationshipAndAddNewTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "StudentGroupId", "dbo.StudentGroups");
            DropIndex("dbo.Students", new[] {"StudentGroupId"});
            CreateTable(
                    "dbo.IdentityStudents",
                    c => new
                    {
                        PersonId = c.String(false, 128),
                        PersonGroupId = c.String(false, 100),
                        StudentId = c.String(false, 128)
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Students", t => t.StudentId, true)
                .Index(t => t.PersonId)
                .Index(t => t.StudentId);

            CreateTable(
                    "dbo.StudentFaces",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Uri = c.String(false),
                        SecureUri = c.String(false),
                        PersonId = c.String(false, 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityStudents", t => t.PersonId)
                .Index(t => t.Id)
                .Index(t => t.PersonId);

            CreateTable(
                    "dbo.StudentsInGroup",
                    c => new
                    {
                        StudentId = c.String(false, 128),
                        StudentGroupId = c.String(false, 128)
                    })
                .PrimaryKey(t => new {t.StudentId, t.StudentGroupId})
                .ForeignKey("dbo.StudentGroups", t => t.StudentId, true)
                .ForeignKey("dbo.Students", t => t.StudentGroupId, true)
                .Index(t => t.StudentId)
                .Index(t => t.StudentGroupId);

            DropColumn("dbo.Students", "StudentGroupId");
        }

        public override void Down()
        {
            AddColumn("dbo.Students", "StudentGroupId", c => c.String(false, 128));
            DropForeignKey("dbo.StudentFaces", "PersonId", "dbo.IdentityStudents");
            DropForeignKey("dbo.IdentityStudents", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentsInGroup", "StudentGroupId", "dbo.Students");
            DropForeignKey("dbo.StudentsInGroup", "StudentId", "dbo.StudentGroups");
            DropIndex("dbo.StudentsInGroup", new[] {"StudentGroupId"});
            DropIndex("dbo.StudentsInGroup", new[] {"StudentId"});
            DropIndex("dbo.StudentFaces", new[] {"PersonId"});
            DropIndex("dbo.StudentFaces", new[] {"Id"});
            DropIndex("dbo.IdentityStudents", new[] {"StudentId"});
            DropIndex("dbo.IdentityStudents", new[] {"PersonId"});
            DropTable("dbo.StudentsInGroup");
            DropTable("dbo.StudentFaces");
            DropTable("dbo.IdentityStudents");
            CreateIndex("dbo.Students", "StudentGroupId");
            AddForeignKey("dbo.Students", "StudentGroupId", "dbo.StudentGroups", "Id");
        }
    }
}