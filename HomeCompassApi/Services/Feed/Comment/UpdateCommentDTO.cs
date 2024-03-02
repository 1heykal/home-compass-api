using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Services.Feed.Comment
{
    public class UpdateCommentDTO
    {
        [Required]
        public string Content { get; set; }
    }
}
