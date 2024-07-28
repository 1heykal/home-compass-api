using HomeCompassApi.Entities.Facilities;

namespace HomeCompassApi.Models.Facilities
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
        public string PhotoUrl { get; set; }

    }
}
