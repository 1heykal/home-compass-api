using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeCompassApi.Models.Feed
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        public int PostId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

        //public int UserId { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public ApplicationUser User { get; set; }
    }
}
