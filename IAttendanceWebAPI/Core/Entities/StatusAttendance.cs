using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Entities
{
    public enum StatusAttendanceType
    {
        NotYet = 1,
        Attended = 2,
        Absent = 3
    }

    public sealed class StatusAttendance
    {
        public StatusAttendance()
        {
            Attendances = new HashSet<Attendance>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}