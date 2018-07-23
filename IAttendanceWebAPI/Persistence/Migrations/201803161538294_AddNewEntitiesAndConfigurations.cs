using System.Data.Entity.Migrations;

namespace IAttendanceWebAPI.Migrations
{
    public partial class AddNewEntitiesAndConfigurations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Attendances",
                    c => new
                    {
                        TimeTableId = c.Int(false),
                        StudentId = c.String(false, 128),
                        StatusAttendanceId = c.Int(false)
                    })
                .PrimaryKey(t => new {t.TimeTableId, t.StudentId})
                .ForeignKey("dbo.StatusAttendances", t => t.StatusAttendanceId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.TimeTables", t => t.TimeTableId)
                .Index(t => new {t.TimeTableId, t.StudentId})
                .Index(t => t.StatusAttendanceId);

            CreateTable(
                    "dbo.StatusAttendances",
                    c => new
                    {
                        Id = c.Int(false),
                        Name = c.String(false, 100)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                    "dbo.Students",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Batch = c.String(false, 20),
                        ImagePath = c.String(false),
                        UserId = c.String(false, 128),
                        StudentGroupId = c.String(false, 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
                .Index(t => t.Id)
                .Index(t => t.UserId)
                .Index(t => t.StudentGroupId);

            CreateTable(
                    "dbo.StudentGroups",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Name = c.String(false, 100)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                    "dbo.TimeTables",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Date = c.DateTime(false),
                        TimeSlotId = c.Int(false),
                        TeacherId = c.String(false, 128),
                        StudentGroupId = c.String(maxLength: 128),
                        RoomId = c.String(false, 128),
                        CampusId = c.Int(false),
                        CourseId = c.String(false, 128),
                        StatusTimeTableId = c.Int(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Campus", t => t.CampusId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .ForeignKey("dbo.StatusTimeTables", t => t.StatusTimeTableId)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroupId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .ForeignKey("dbo.TimeSlots", t => t.TimeSlotId)
                .Index(t => t.Id)
                .Index(t => t.TimeSlotId)
                .Index(t => t.TeacherId)
                .Index(t => t.StudentGroupId)
                .Index(t => t.RoomId)
                .Index(t => t.CampusId)
                .Index(t => t.CourseId)
                .Index(t => t.StatusTimeTableId);

            CreateTable(
                    "dbo.Campus",
                    c => new
                    {
                        Id = c.Int(false),
                        Name = c.String(false, 250)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                    "dbo.Courses",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Name = c.String(false, 250)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                    "dbo.Rooms",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Name = c.String(false, 200)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                    "dbo.StatusTimeTables",
                    c => new
                    {
                        Id = c.Int(false),
                        Name = c.String(false, 100)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                    "dbo.Teachers",
                    c => new
                    {
                        Id = c.String(false, 128),
                        UserId = c.String(false, 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
                .Index(t => t.Id)
                .Index(t => t.UserId);

            CreateTable(
                    "dbo.TimeSlots",
                    c => new
                    {
                        Id = c.Int(false),
                        StartTime = c.Time(false, 7),
                        EndTime = c.Time(false, 7)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            AddColumn("dbo.AspNetUsers", "Name", c => c.String(false, 100));
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime(false));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(maxLength: 100));
        }

        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "TimeTableId", "dbo.TimeTables");
            DropForeignKey("dbo.TimeTables", "TimeSlotId", "dbo.TimeSlots");
            DropForeignKey("dbo.Teachers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TimeTables", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TimeTables", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.TimeTables", "StatusTimeTableId", "dbo.StatusTimeTables");
            DropForeignKey("dbo.TimeTables", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.TimeTables", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.TimeTables", "CampusId", "dbo.Campus");
            DropForeignKey("dbo.Attendances", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Students", "StudentGroupId", "dbo.StudentGroups");
            DropForeignKey("dbo.Attendances", "StatusAttendanceId", "dbo.StatusAttendances");
            DropIndex("dbo.TimeSlots", new[] {"Id"});
            DropIndex("dbo.Teachers", new[] {"UserId"});
            DropIndex("dbo.Teachers", new[] {"Id"});
            DropIndex("dbo.StatusTimeTables", new[] {"Id"});
            DropIndex("dbo.Rooms", new[] {"Id"});
            DropIndex("dbo.Courses", new[] {"Id"});
            DropIndex("dbo.Campus", new[] {"Id"});
            DropIndex("dbo.TimeTables", new[] {"StatusTimeTableId"});
            DropIndex("dbo.TimeTables", new[] {"CourseId"});
            DropIndex("dbo.TimeTables", new[] {"CampusId"});
            DropIndex("dbo.TimeTables", new[] {"RoomId"});
            DropIndex("dbo.TimeTables", new[] {"StudentGroupId"});
            DropIndex("dbo.TimeTables", new[] {"TeacherId"});
            DropIndex("dbo.TimeTables", new[] {"TimeSlotId"});
            DropIndex("dbo.TimeTables", new[] {"Id"});
            DropIndex("dbo.StudentGroups", new[] {"Id"});
            DropIndex("dbo.Students", new[] {"StudentGroupId"});
            DropIndex("dbo.Students", new[] {"UserId"});
            DropIndex("dbo.Students", new[] {"Id"});
            DropIndex("dbo.StatusAttendances", new[] {"Id"});
            DropIndex("dbo.Attendances", new[] {"StatusAttendanceId"});
            DropIndex("dbo.Attendances", new[] {"TimeTableId", "StudentId"});
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.TimeSlots");
            DropTable("dbo.Teachers");
            DropTable("dbo.StatusTimeTables");
            DropTable("dbo.Rooms");
            DropTable("dbo.Courses");
            DropTable("dbo.Campus");
            DropTable("dbo.TimeTables");
            DropTable("dbo.StudentGroups");
            DropTable("dbo.Students");
            DropTable("dbo.StatusAttendances");
            DropTable("dbo.Attendances");
        }
    }
}