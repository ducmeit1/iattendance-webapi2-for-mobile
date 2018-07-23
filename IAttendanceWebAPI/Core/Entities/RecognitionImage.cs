namespace IAttendanceWebAPI.Core.Entities
{
    public class RecognitionImage
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public string SecureUri { get; set; }
        public int TimeTableId { get; set; }
        public TimeTable TimeTable { get; set; }
    }
}