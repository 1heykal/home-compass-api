namespace HomeCompassApi.Models.User
{
    public class UserDetailsDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public string PhotoURL { get; set; }

    }
}
