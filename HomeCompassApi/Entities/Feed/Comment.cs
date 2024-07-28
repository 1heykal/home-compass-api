using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeCompassApi.Entities.Feed
{
    public class Comment
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        [JsonIgnore]
        public Post Post { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public ApplicationUser User { get; set; }
    }
}
