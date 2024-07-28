using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Entities.Auth
{
    public class TokenRequestModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
