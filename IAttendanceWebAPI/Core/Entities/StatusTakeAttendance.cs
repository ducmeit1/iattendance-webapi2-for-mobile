using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Entities
{
    public enum StatusTakeAttendanceType
    {
        Waiting = 1,
        Taken = 2
    }

    public sealed class StatusTakeAttendance
    {
        public StatusTakeAttendance()
        {
            TimeTables = new HashSet<TimeTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TimeTable> TimeTables { get; set; }
    }
}