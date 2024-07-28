namespace HomeCompassApi.Models.Feed
{
    public class ReadAllReportsDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public bool Archived { get; set; }
        public string ReporterId { get; set; }
        public int PostId { get; set; }

    }
}
