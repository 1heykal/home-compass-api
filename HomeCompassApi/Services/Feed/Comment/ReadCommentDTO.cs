using HomeCompassApi.Models.Feed;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeCompassApi.Services.CRUD
{
    public class ReadCommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
    }
}
