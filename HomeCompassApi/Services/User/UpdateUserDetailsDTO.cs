namespace HomeCompassApi.Services.User
{
    public class UpdateUserDetailsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string PhotoURL { get; set; }
        public string Address { get; set; }
    }
}
