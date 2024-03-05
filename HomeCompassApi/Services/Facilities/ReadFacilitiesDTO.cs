using HomeCompassApi.Models.Facilities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Services.Facilities
{
    public class ReadFacilitiesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInformaton { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public List<Resource> Resources { get; set; }
        public string Target { get; set; }

    }
}
