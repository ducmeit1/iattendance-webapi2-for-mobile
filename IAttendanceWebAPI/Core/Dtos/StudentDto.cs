using Newtonsoft.Json;
using System.Collections.Generic;

namespace IAttendanceWebAPI.Core.Dtos
{
    public class StudentDto
    {
        public StudentDto()
        {
            StudentGroups = new HashSet<StudentGroupDto>();
        }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("batch")] public string Batch { get; set; }

        [JsonProperty("image_path")] public string ImagePath { get; set; }
        [JsonProperty("student_groups")] public ICollection<StudentGroupDto> StudentGroups { get; set; }

        [JsonProperty("student_detail")] public UserDto User { get; set; }
    }
}