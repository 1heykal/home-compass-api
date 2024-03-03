using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Services.Feed
{
    public class UpdateReportDTO
    {
        [Required]
        public string Type { get; set; }
        public string Details { get; set; }
        public bool Archived { get; set; }

    }
}
