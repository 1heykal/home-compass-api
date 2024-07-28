namespace HomeCompassApi.Models.Facilities
{
    public class ReadJobsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Skills { get; set; }
        public int Hours { get; set; }
        public decimal Salary { get; set; }
        public string Location { get; set; }
        public string ContactInformation { get; set; }
        public int CategoryId { get; set; }
        public string ContributorId { get; set; }

    }
}
