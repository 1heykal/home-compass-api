namespace HomeCompassApi.Services.Feed
{
    public class PostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPhotoUrl { get; set; }
        public int CommentsCount { get; set; }
    }
}
