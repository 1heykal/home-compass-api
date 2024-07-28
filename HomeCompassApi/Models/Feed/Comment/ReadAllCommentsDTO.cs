namespace HomeCompassApi.Models.Feed.Comment
{
    public class ReadAllCommentsDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPhotoURL { get; set; }

    }
}
