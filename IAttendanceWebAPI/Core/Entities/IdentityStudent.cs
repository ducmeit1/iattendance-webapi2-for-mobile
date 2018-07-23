using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Entities
{
    public class IdentityStudent
    {
        public IdentityStudent()
        {
            StudentFaces = new HashSet<StudentFace>();
        }

        public string PersonId { get; set; }
        public string PersonGroupId { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public ICollection<StudentFace> StudentFaces { get; set; }
    }
}