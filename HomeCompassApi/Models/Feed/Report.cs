using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeCompassApi.Models.Feed
{
    public class Report
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public bool Archived { get; set; }

        public string ReporterId { get; set; } = string.Empty;

        [ForeignKey(nameof(ReporterId))]
        [JsonIgnore]
        public ApplicationUser Reporter { get; set; }

        [Required]
        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        [JsonIgnore]
        public Post Post { get; set; }
    }
}
