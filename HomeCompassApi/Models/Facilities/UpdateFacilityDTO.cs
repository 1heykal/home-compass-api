using System.ComponentModel.DataAnnotations;
using HomeCompassApi.Entities.Facilities;

namespace HomeCompassApi.Models.Facilities
{
    public class UpdateFacilityDTO
    {
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public string ContactInformation { get; set; }
        public int CategoryId { get; set; }
        public int Hours { get; set; }
        public List<Resource> Resources { get; set; }
        public List<string> Days { get; set; }
        public string PhotoUrl { get; set; }


    }
}
