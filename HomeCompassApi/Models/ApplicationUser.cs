using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeCompassApi.Models.Auth;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Services.User;
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
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }

        public string EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTokenExpiresAt { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiresAt { get; set; }
        public bool PasswordTokenConfirmed { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Facility> Facilities { get; set; }
        public ICollection<Resource> Resources { get; set; }


        public ApplicationUser()
        {

        }


        public ApplicationUser(UpdateUserDetailsDTO userDetailsDTO)
        {
            FirstName = userDetailsDTO.FirstName;
            LastName = userDetailsDTO.LastName;
            Gender = userDetailsDTO.Gender;
            Address = userDetailsDTO.Address;
            Email = userDetailsDTO.Email;
            BirthDate = userDetailsDTO.BirthDate;
            PhotoUrl = userDetailsDTO.PhotoURL;

        }
    }
}
