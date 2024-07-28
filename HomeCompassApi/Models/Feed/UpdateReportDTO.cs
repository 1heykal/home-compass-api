using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Models.Feed
{
    public class UpdateReportDTO
    {
        [Required]
        public string Type { get; set; }
        public string Details { get; set; }
        public bool Archived { get; set; }

    }
}
