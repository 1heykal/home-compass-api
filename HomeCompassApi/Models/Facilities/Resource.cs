using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeCompassApi.Models.Facilities
{
    public class Resource
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        public List<Facility> Facilities { get; set; }
    }
}
