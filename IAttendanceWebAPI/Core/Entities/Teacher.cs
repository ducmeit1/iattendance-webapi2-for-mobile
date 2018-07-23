using Newtonsoft.Json;
using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Entities
{
    public sealed class Teacher
    {
        public Teacher()
        {
            TimesTables = new HashSet<TimeTable>();
        }

        [JsonProperty("teacher_id")] public string Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<TimeTable> TimesTables { get; set; }
    }
}