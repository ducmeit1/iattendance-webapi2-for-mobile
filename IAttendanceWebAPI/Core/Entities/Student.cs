using Newtonsoft.Json;
using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Entities
{
    public sealed class Student
    {
        public Student()
        {
            StudentGroups = new HashSet<StudentGroup>();
            Attendances = new HashSet<Attendance>();
        }

        [JsonProperty("student_id")] public string Id { get; set; }

        public string Batch { get; set; }
        public string ImagePath { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<StudentGroup> StudentGroups { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}