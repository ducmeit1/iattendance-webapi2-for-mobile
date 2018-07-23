using IAttendanceWebAPI.Core.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace IAttendanceWebAPI.Persistence.Migrations
{
    public class SeedingData
    {
        public static void SeedComplexData(ApplicationDbContext context)
        {
            /*Seed data to Tables: Courses, Rooms, Campuses, StudentGroups, StatusAttendances, StatusTimeTables, TimeSlots*/
            context.Courses.AddOrUpdate(c => c.Id,
                new Course { Id = "PRM391", Name = "Programing with mobile" },
                new Course { Id = "SWD391", Name = "Software Architecture and Design" },
                new Course { Id = "ACC101", Name = "Principles of Accounting" },
                new Course { Id = "ISC301", Name = "E-Commerce" },
                new Course { Id = "HCI201", Name = "Human-Computer Interaction" }
            );

            context.Rooms.AddOrUpdate(r => r.Id,
                new Room { Id = "P201", Name = "201 - Beta" },
                new Room { Id = "P202", Name = "202 - Beta" },
                new Room { Id = "P203", Name = "203 - Beta" },
                new Room { Id = "HB201/R", Name = "201 - Alpha Right" },
                new Room { Id = "HB201/L", Name = "201 - Alpha Left" }
            );

            context.Campuses.AddOrUpdate(c => c.Id,
                new Campus { Id = 1, Name = "HL-Hoa Lac" },
                new Campus { Id = 2, Name = "HCM-Ho Chi Minh" },
                new Campus { Id = 3, Name = "CT-CanTho" },
                new Campus { Id = 4, Name = "DN-Da Nang" }
            );

            context.StudentGroups.AddOrUpdate(s => s.Id,
                new StudentGroup { Id = "IS1101", Name = "K10 - IS" },
                new StudentGroup { Id = "IS1102", Name = "K10 - IS" },
                new StudentGroup { Id = "IS1103", Name = "K10 - IS" }
            );

            context.StatusAttendances.AddOrUpdate(s => s.Id,
                new StatusAttendance { Id = 1, Name = "Not yet" },
                new StatusAttendance { Id = 2, Name = "Attended" },
                new StatusAttendance { Id = 3, Name = "Absent" }
            );

            context.StatusTimeTables.AddOrUpdate(s => s.Id,
                new StatusTimeTable { Id = 1, Name = "Booked" },
                new StatusTimeTable { Id = 2, Name = "Happening" },
                new StatusTimeTable { Id = 3, Name = "Finished" },
                new StatusTimeTable { Id = 4, Name = "Canceled" },
                new StatusTimeTable { Id = 5, Name = "Changed Schedule" },
                new StatusTimeTable { Id = 6, Name = "Changed Room" }
            );

            context.TimeSlots.AddOrUpdate(s => s.Id,
                new TimeSlot { Id = 1, StartTime = new TimeSpan(7, 30, 0), EndTime = new TimeSpan(9, 0, 0) },
                new TimeSlot { Id = 2, StartTime = new TimeSpan(9, 10, 0), EndTime = new TimeSpan(10, 40, 0) },
                new TimeSlot { Id = 3, StartTime = new TimeSpan(10, 50, 0), EndTime = new TimeSpan(12, 20, 0) },
                new TimeSlot { Id = 4, StartTime = new TimeSpan(12, 50, 0), EndTime = new TimeSpan(14, 20, 0) },
                new TimeSlot { Id = 5, StartTime = new TimeSpan(14, 30, 0), EndTime = new TimeSpan(16, 0, 0) },
                new TimeSlot { Id = 6, StartTime = new TimeSpan(16, 10, 0), EndTime = new TimeSpan(17, 40, 0) },
                new TimeSlot { Id = 7, StartTime = new TimeSpan(18, 00, 0), EndTime = new TimeSpan(19, 30, 0) },
                new TimeSlot { Id = 8, StartTime = new TimeSpan(19, 45, 0), EndTime = new TimeSpan(21, 15, 0) }
            );

            context.SaveChanges();
        }

        public static void SeedUser(ApplicationDbContext context)
        {
            var users = context.Users.ToList();
            foreach (var u in users)
                context.Users.Remove(u);
            context.SaveChanges();

            var userManager = new ApplicationUserManager(new UserStore<User>(context));
            var passwordHasher = userManager.PasswordHasher;

            var user1 = new User
            {
                Name = "Ho Trong Duc",
                Email = "duchtse61924@fpt.edu.vn",
                PasswordHash = passwordHasher.HashPassword("Abc12345!"),
                UserName = "duchtse61924@fpt.edu.vn",
                DateOfBirth = new DateTime(1996, 9, 12),
                PhoneNumber = "01666210190"
            };
            var user2 = new User
            {
                Name = "Ngo Thuc Dat",
                Email = "datntse62120@fpt.edu.vn",
                UserName = "datntse62120@fpt.edu.vn",
                PasswordHash = passwordHasher.HashPassword("Abc12345!"),
                DateOfBirth = new DateTime(1996, 2, 3),
                PhoneNumber = "01654541210"
            };
            var user3 = new User
            {
                Name = "Nguyen Quy",
                Email = "quyn@fpt.edu.vn",
                UserName = "quyn@fpt.edu.vn",
                DateOfBirth = new DateTime(1995, 10, 20),
                PasswordHash = passwordHasher.HashPassword("Abc12345!"),
                PhoneNumber = "09850502015"
            };

            var user4 = new User
            {
                Name = "Bui Ngoc Anh",
                Email = "anhbn@fpt.edu.vn",
                UserName = "anhbn@fpt.edu.vn",
                DateOfBirth = new DateTime(1965, 10, 20),
                PasswordHash = passwordHasher.HashPassword("Abc12345!"),
                PhoneNumber = "09145452121"
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user4);

            context.SaveChanges();


            context.Students.AddOrUpdate(s => s.Id,
                new Student
                {
                    UserId = user1.Id,
                    Id = "SE61924",
                    Batch = "K10",
                    ImagePath = "http://res.cloudinary.com/ducmeit1/image/upload/v1521189182/sample.jpg"
                },
                new Student
                {
                    UserId = user2.Id,
                    Id = "SE62120",
                    Batch = "K10",
                    ImagePath = "http://res.cloudinary.com/ducmeit1/image/upload/v1521189182/sample.jpg"
                },
                new Student
                {
                    UserId = user3.Id,
                    Id = "SE40598",
                    Batch = "K9",
                    ImagePath = "http://res.cloudinary.com/ducmeit1/image/upload/v1521189182/sample.jpg"
                });

            context.Teachers.AddOrUpdate(t => t.Id,
                new Teacher { UserId = user4.Id, Id = "AnhBNSE1101" });

            context.SaveChanges();
        }

        public static void UpdateUserSecurityStamp(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<User>(context));
            var users = context.Users.Select(u => u.Id).ToArray();
            foreach (var id in users) userManager.UpdateSecurityStamp(id);
        }

        public static void SeedTimeTable(ApplicationDbContext context)
        {
            context.TimeTables.AddOrUpdate(t => t.Id,
                new TimeTable
                {
                    CampusId = 1,
                    CourseId = "PRM391",
                    Date = DateTime.Now,
                    RoomId = "P201",
                    StatusTimeTableId = 1,
                    StudentGroupId = "IS1101",
                    TeacherId = "AnhBNSE1101",
                    TimeSlotId = 1,
                    StatusTakeAttendanceId = 1
                },
                new TimeTable
                {
                    CampusId = 1,
                    CourseId = "PRM391",
                    Date = DateTime.Now,
                    RoomId = "P201",
                    StatusTimeTableId = 1,
                    StudentGroupId = "IS1101",
                    TeacherId = "AnhBNSE1101",
                    TimeSlotId = 2,
                    StatusTakeAttendanceId = 1
                },
                new TimeTable
                {
                    CampusId = 1,
                    CourseId = "PRM391",
                    Date = DateTime.Now,
                    RoomId = "P201",
                    StatusTimeTableId = 1,
                    StudentGroupId = "IS1101",
                    TeacherId = "AnhBNSE1101",
                    TimeSlotId = 3,
                    StatusTakeAttendanceId = 1
                },
                new TimeTable
                {
                    CampusId = 1,
                    CourseId = "SWD391",
                    Date = DateTime.Now,
                    RoomId = "P202",
                    StatusTimeTableId = 1,
                    StudentGroupId = "IS1102",
                    TeacherId = "AnhBNSE1101",
                    TimeSlotId = 4,
                    StatusTakeAttendanceId = 1
                }
            );

            context.SaveChanges();
        }

        public static void SeedAttendance(ApplicationDbContext context)
        {
            var timeTableIds = context.TimeTables.Select(t => t.Id).ToArray();
            var studentIds = context.Students.Select(s => s.Id).ToArray();
            foreach (var id in studentIds)
                foreach (var ttId in timeTableIds)
                    context.Attendances.Add(
                        new Attendance { StudentId = id, TimeTableId = ttId, StatusAttendanceId = 1 });

            context.SaveChanges();
        }

        public static void SeedStudentsInGroup(ApplicationDbContext context)
        {
            var groups = context.StudentGroups.ToList();
            var students = context.Students.ToList();
            students[0].StudentGroups.Add(groups[0]);
            students[0].StudentGroups.Add(groups[1]);
            context.Entry(students[0]).State = EntityState.Modified;
            context.SaveChanges();
        }

        public static void SeedRoles(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("Teacher"));
            roleManager.Create(new IdentityRole("Student"));
        }

        public static void SeedStatusMessage(ApplicationDbContext context)
        {
            context.StatusMessages.AddOrUpdate(s => s.Id,
                new StatusMessage
                {
                    Id = 0,
                    Name = "Waiting"
                },
                new StatusMessage
                {
                    Id = 1,
                    Name = "Approved"
                },
                new StatusMessage
                {
                    Id = 2,
                    Name = "Rejected"
                });

            context.SaveChanges();
        }

        public static void SeedUsers2(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<User>(context));
            var user1 = new User
            {
                Name = "Bui Ngoc Anh",
                Email = "anhbnse41101@fpt.edu.vn",
                UserName = "anhbnse41101@fpt.edu.vn",
                DateOfBirth = new DateTime(1980, 5, 11),
                PhoneNumber = "01699696969"
            };
            userManager.Create(user1, "Abc12345!");
            //var user2 = new User
            //{
            //    Name = "Hoang Trung Kien",
            //    Email = "kienhtse40111@fpt.edu.vn",
            //    UserName = "kienhtse40111@fpt.edu.vn",
            //    DateOfBirth = new DateTime(1996, 2, 2),
            //    PhoneNumber = "0165442100"
            //};
            //userManager.Create(user2, "Abc12345!");
            //var user3 = new User
            //{
            //    Name = "No Name",
            //    Email = "nonamese40241@fpt.edu.vn",
            //    UserName = "nonamese40241@fpt.edu.vn",
            //    DateOfBirth = new DateTime(1995, 4, 20),
            //    PhoneNumber = "0956541211"
            //};
            //userManager.Create(user3, "Abc12345!");
        }
    }
}