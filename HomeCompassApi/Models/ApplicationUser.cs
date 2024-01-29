using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using Microsoft.AspNetCore.Identity;

namespace HomeCompassApi.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.ImageUrl)]
        public string PhotoUrl { get; set; }

        public string Address { get; set; }

        [Range(minimum: 17, maximum: 100)]
        public int Age { get; set; }

        public string Gender { get; set; }

        public string Role { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Facility> Facilities { get; set; }
        public ICollection<Resource> Resources { get; set; }
    }
}
