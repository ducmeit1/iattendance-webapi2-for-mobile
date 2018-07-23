using IAttendanceWebAPI.Core.Dtos;
using IAttendanceWebAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Core.Repositories
{
    public interface ITimeTableRepository : IRepository<TimeTable>
    {
        Task<TimeTable> GetTimeTable(int id);
        Task<IEnumerable<TeacherTimeTableDto>> GetTimeTablesForTeacherByDate(string teacherId, DateTime date);
        Task<IEnumerable<StudentTimeTableDto>> GetTimeTablesForStudentByDate(string studentId, DateTime date);
    }
}