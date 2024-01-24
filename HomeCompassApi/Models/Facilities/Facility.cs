using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HomeCompassApi.Models.Facilities
{
    public class Facility
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public byte[] Photo { get; set; }

        public bool canHelp;
        public ICollection<Shelter> Shelters { get; set; }
        public ICollection<Restaurant> Restaurants { get; set; }

    }
}
