using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Entities
{
    public enum StatusTimeTableType
    {
        Booked = 1,
        Happening = 2,
        Finished = 3,
        Canceled = 4,
        ChangedSchedule = 5,
        ChangedRoom = 6
    }

    public sealed class StatusTimeTable
    {
        public StatusTimeTable()
        {
            TimeTables = new HashSet<TimeTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TimeTable> TimeTables { get; set; }
    }
}