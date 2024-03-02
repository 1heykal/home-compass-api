namespace HomeCompassApi.Services.CRUD
{
    public class ReadAllPostsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPhotoUrl { get; set; }
        public int CommentsCount { get; set; }
        public int LikesCount { get; set; }

    }
}
