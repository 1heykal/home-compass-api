using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeCompassApi.Models
{
    public class Info
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
