
namespace HomeCompassApi.Models.Feed.Comment
{
    public class ReadCommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
    }
}
