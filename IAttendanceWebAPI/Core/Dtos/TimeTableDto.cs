using Newtonsoft.Json;
using System;

namespace IAttendanceWebAPI.Core.Dtos
{
    public class StudentTimeTableDto
    {
        [JsonProperty("student_id")] public string StudentId { get; set; }

        [JsonProperty("time_table_id")] public int Id { get; set; }

        [JsonProperty("date")] public DateTime Date { get; set; }

        [JsonProperty("slot")] public TimeSlotDto TimeSlot { get; set; }

        [JsonProperty("teacher_id")] public string TeacherId { get; set; }

        [JsonProperty("student_group")] public StudentGroupDto StudentGroup { get; set; }

        [JsonProperty("room")] public RoomDto Room { get; set; }

        [JsonProperty("campus")] public CampusDto Campus { get; set; }

        [JsonProperty("course")] public CourseDto Course { get; set; }

        [JsonProperty("status_time_table")] public StatusTimeTableDto StatusTimeTable { get; set; }

        [JsonProperty("status_attendance")] public StatusAttendanceDto StatusAttendance { get; set; }
    }

    public class TeacherTimeTableDto
    {
        [JsonProperty("time_table_id")] public int Id { get; set; }

        [JsonProperty("date")] public DateTime Date { get; set; }

        [JsonProperty("slot")] public TimeSlotDto TimeSlot { get; set; }

        [JsonProperty("teacher_id")] public string TeacherId { get; set; }

        [JsonProperty("student_group")] public StudentGroupDto StudentGroup { get; set; }

        [JsonProperty("room")] public RoomDto Room { get; set; }

        [JsonProperty("campus")] public CampusDto Campus { get; set; }

        [JsonProperty("course")] public CourseDto Course { get; set; }

        [JsonProperty("status_time_table")] public StatusTimeTableDto StatusTimeTable { get; set; }

        [JsonProperty("status_take_attendance")]
        public StatusTakeAttendanceDto StatusTakeAttendance { get; set; }
    }
}