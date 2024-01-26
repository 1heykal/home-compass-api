using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Models.Cases
{
    public class Case
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.ImageUrl)]
        public string PhotoUrl { get; set; }
        public bool Archived { get; set; }
    }
}
