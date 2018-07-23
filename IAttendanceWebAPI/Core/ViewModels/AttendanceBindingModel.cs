using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace IAttendanceWebAPI.Core.ViewModels
{
    public class AttendanceBindingModel
    {
        [JsonProperty("student_id")]
        [Required]
        [Display(Name = "Student id")]
        public string StudentId { get; set; }

        [JsonProperty("status_attendance_id")]
        [Required]
        [Display(Name = "Status attendance")]
        public int StatusAttendanceId { get; set; }
    }
}