using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Entities.Auth
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
