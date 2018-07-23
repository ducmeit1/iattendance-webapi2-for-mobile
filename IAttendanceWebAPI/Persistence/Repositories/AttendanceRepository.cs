using AutoMapper;
using IAttendanceWebAPI.Core.Dtos;
using IAttendanceWebAPI.Core.Entities;
using IAttendanceWebAPI.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendancesListForTimeTable(int timeTableId)
        {
            var attendance = await Get(a => a.TimeTableId == timeTableId,
                a => a.OrderBy(at => at.StudentId),
                "Student, Student.User, StatusAttendance");

            return Mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceDto>>(attendance);
        }

        public async Task<Attendance> GetAttendanceForStudent(int timeTableId, string studentId)
        {
            return await Get(a => a.TimeTableId == timeTableId && a.StudentId == studentId, "StatusAttendance");
        }

        public async Task<object> UpdateAttendanceForStudent(Attendance attendance)
        {
            Update(attendance);
            await Completed();
            return new
            {
                student_id = attendance.StudentId,
                id = attendance.StatusAttendance.Id,
                name = attendance.StatusAttendance.Name
            };
        }

        public async Task<IEnumerable<object>> UpdateAttendanceForStudentList(IEnumerable<Attendance> attendancesList)
        {
            var attendances = attendancesList.ToList();
            foreach (var attendance in attendances) Update(attendance);

            await Completed();

            return attendances.Select(c =>
                new {student_id = c.StudentId, id = c.StatusAttendance.Id, name = c.StatusAttendance.Name});
        }

        public async Task<int> CountTotalStudentsOfATimeTable(int timeTableId)
        {
            var studentsList = await (from a in DbContext.Attendances
                where a.TimeTableId == timeTableId
                select a.StudentId).ToListAsync();

            return studentsList.Count;
        }

        public async Task<IEnumerable<Attendance>> GetAttendanceListForStudentsList(int timeTableId,
            IEnumerable<string> studentsIdList)
        {
            var query = from a in DbContext.Attendances
                where a.TimeTableId == timeTableId && studentsIdList.Contains(a.StudentId)
                select a;

            return await Get(query, includeProperties: "StatusAttendance");
        }

        public async Task<IEnumerable<string>> GetStudentIdListForTimeTable(int timeTableId)
        {
            var studentIds = await (from a in Entities
                where a.TimeTableId == timeTableId
                select a.StudentId).OrderBy(t => t).ToListAsync();
            return studentIds;
        }
    }
}