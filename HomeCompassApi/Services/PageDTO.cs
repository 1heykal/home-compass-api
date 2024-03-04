using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Services
{
    public class PageDTO
    {
        [Range(1, int.MaxValue)]
        [Required]
        public int Index { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        public int Size { get; set; }

    }
}
