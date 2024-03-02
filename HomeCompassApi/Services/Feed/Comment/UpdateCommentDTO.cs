using HomeCompassApi.Models.Feed;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeCompassApi.Services.CRUD
{
    public class UpdateCommentDTO
    {
        public string Content { get; set; }
    }
}
