using HomeCompassApi.Models.Facilities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Services.Facilities
{
    public class ReadJobsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Skills { get; set; }
        public string Location { get; set; }
        public string ContactInformation { get; set; }
        public int CategoryId { get; set; }
        public string ContributorId { get; set; }

    }
}
