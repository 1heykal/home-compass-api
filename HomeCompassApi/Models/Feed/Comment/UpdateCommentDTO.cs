using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Models.Feed.Comment
{
    public class UpdateCommentDTO
    {
        [Required]
        public string Content { get; set; }
    }
}
