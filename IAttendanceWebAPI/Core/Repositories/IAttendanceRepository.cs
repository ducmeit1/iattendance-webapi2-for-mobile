using IAttendanceWebAPI.Core.Dtos;
using IAttendanceWebAPI.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Core.Repositories
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        Task<IEnumerable<AttendanceDto>> GetAttendancesListForTimeTable(int timeTableId);
        Task<Attendance> GetAttendanceForStudent(int timeTableId, string studentId);
        Task<object> UpdateAttendanceForStudent(Attendance attendance);
        Task<IEnumerable<object>> UpdateAttendanceForStudentList(IEnumerable<Attendance> attendancesList);
        Task<int> CountTotalStudentsOfATimeTable(int timeTableId);

        Task<IEnumerable<Attendance>> GetAttendanceListForStudentsList(int timeTableId,
            IEnumerable<string> studentsListId);

        Task<IEnumerable<string>> GetStudentIdListForTimeTable(int timeTableId);
    }
}