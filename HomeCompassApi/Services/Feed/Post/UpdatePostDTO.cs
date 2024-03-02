namespace HomeCompassApi.Services.CRUD
{
    public class UpdatePostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Archived { get; set; } = false;

    }
}
