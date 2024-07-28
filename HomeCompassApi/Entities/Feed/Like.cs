using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Entities.Feed
{
    [PrimaryKey(nameof(UserId), nameof(PostId))]
    public class Like
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public ApplicationUser User { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        [JsonIgnore]
        public Post Post { get; set; }
    }
}