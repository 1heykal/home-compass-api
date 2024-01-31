using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Models
{
    public class RegisterModel
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }
        [Required, StringLength(25)]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
