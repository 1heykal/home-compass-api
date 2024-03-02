namespace HomeCompassApi.Services.CRUD
{
    public class ReadPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CommentsCount { get; set; }
        public int LikesCount { get; set; }
        public DateTime PublisedOn { get; set; }
        public bool Archived { get; set; } = false;
        public string UserId { get; set; }
    }
}
