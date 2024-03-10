namespace HomeCompassApi.Models.Auth
{
    public class ChangePasswordModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}