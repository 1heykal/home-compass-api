using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeCompassApi.Models.Feed
{
    [PrimaryKey(nameof(UserId), nameof(PostId))]
    public class Like
    {
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public ApplicationUser User { get; set; }

        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        [JsonIgnore]
        public Post Post { get; set; }
    }
}