using System.ComponentModel.DataAnnotations;

namespace HomeCompassApi.Models.Facilities
{
    public class UpdateJobDTO
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public List<string> Skills { get; set; }
        public string Location { get; set; }
        public List<string> Days { get; set; }
        public int Hours { get; set; }
        public string ContactInformation { get; set; }
        public string Benefits { get; set; }
    }
}
