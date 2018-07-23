using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Entities
{
    public sealed class Campus
    {
        public Campus()
        {
            TimeTables = new HashSet<TimeTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TimeTable> TimeTables { get; set; }
    }
}