using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Entities
{
    public sealed class StudentGroup
    {
        public StudentGroup()
        {
            Students = new HashSet<Student>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}