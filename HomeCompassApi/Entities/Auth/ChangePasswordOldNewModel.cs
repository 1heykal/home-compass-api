using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Entities.Auth
{
    public class ChangePasswordOldNewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}