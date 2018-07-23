using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IAttendanceWebAPI.Core.ViewModels
{
    public class IdentifiedPersonModel
    {
        public string PersonId { get; set; }
        public string Name { get; set; }
    }

    public class RecognizeFacesBindingModel
    {
        public string PersonGroupId { get; set; }
        public IEnumerable<string> FacesUrl { get; set; }
    }

    public class PersonGroupBindingModel
    {
        [JsonProperty("id")] [Required] public string Id { get; set; }

        [JsonProperty("name")] [Required] public string Name { get; set; }
    }

    public class AddFacesBindingModel
    {
        public string PersonGroupId { get; set; }
        public Guid PersonId { get; set; }
        public IEnumerable<string> FacesUrl { get; set; }
    }

    public class PersonBindingModel
    {
        public string PersonGroupId { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> FacesUrl { get; set; }
    }
}