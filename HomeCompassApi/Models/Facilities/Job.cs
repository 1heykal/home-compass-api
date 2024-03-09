using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeCompassApi.Models.Facilities
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public List<string> Skills { get; set; }
        public string Location { get; set; }
        public List<string> Days { get; set; }
        public int Hours { get; set; }
        public string ContactInformation { get; set; }
        public string Benefits { get; set; }
        public int EmployerId { get; set; }

        [ForeignKey(nameof(EmployerId))]
        public Facility Facility { get; set; }
    }
}
