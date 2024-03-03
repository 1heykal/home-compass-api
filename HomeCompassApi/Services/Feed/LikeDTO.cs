using HomeCompassApi.Models.Feed;
using HomeCompassApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeCompassApi.Services.Feed
{
    public class LikeDTO
    {
        public string UserId { get; set; }
        public int PostId { get; set; }

    }
}
