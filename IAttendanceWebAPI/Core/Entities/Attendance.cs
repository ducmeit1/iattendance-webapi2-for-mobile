namespace IAttendanceWebAPI.Core.Entities
{
    public sealed class Attendance
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public int TimeTableId { get; set; }
        public TimeTable TimeTable { get; set; }
        public int StatusAttendanceId { get; set; }
        public StatusAttendance StatusAttendance { get; set; }
    }
}