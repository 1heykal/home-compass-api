﻿namespace HomeCompassApi.Models.Facilities
{
    public class ReadJobDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public List<string> Skills { get; set; }
        public string Location { get; set; }
        public List<string> Days { get; set; }
        public int Hours { get; set; }
        public string ContactInformation { get; set; }
        public string Benefits { get; set; }
        public int ContributorId { get; set; }
    }
}
