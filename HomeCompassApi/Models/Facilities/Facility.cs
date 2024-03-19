using HomeCompassApi.Repositories.Facilities;
using HomeCompassApi.Services.Facilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeCompassApi.Models.Facilities
{
    public class Facility
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; } = 0;

        [ForeignKey(nameof(CategoryId))]
        [JsonIgnore]
        public Category Category { get; set; }
        public string Target { get; set; }
        public List<Resource> Resources { get; set; }
        public List<string> Days { get; set; }
        public int Hours { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string ContactInformaton { get; set; }

        public string ContributorId { get; set; } = string.Empty;

        [ForeignKey(nameof(ContributorId))]
        [JsonIgnore]
        public ApplicationUser Contributor { get; set; }

    }
}
