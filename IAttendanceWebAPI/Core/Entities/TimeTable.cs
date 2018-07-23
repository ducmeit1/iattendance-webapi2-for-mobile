using System;
using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Entities
{
    public sealed class TimeTable
    {
        public TimeTable()
        {
            Attendances = new HashSet<Attendance>();
            RecognitionImages = new HashSet<RecognitionImage>();
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string StudentGroupId { get; set; }
        public StudentGroup StudentGroup { get; set; }
        public string RoomId { get; set; }
        public Room Room { get; set; }
        public int CampusId { get; set; }
        public Campus Campus { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public int StatusTimeTableId { get; set; }
        public StatusTimeTable StatusTimeTable { get; set; }
        public int StatusTakeAttendanceId { get; set; }
        public StatusTakeAttendance StatusTakeAttendance { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<RecognitionImage> RecognitionImages { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}