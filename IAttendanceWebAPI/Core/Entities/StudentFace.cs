namespace IAttendanceWebAPI.Core.Entities
{
    public class StudentFace
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public string SecureUri { get; set; }
        public string PersonId { get; set; }
        public IdentityStudent IdentityStudent { get; set; }
    }
}