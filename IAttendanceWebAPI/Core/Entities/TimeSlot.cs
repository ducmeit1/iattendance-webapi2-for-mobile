using System;
using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Entities
{
    public sealed class TimeSlot
    {
        public TimeSlot()
        {
            TimeTables = new HashSet<TimeTable>();
        }

        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ICollection<TimeTable> TimeTables { get; set; }
    }
}