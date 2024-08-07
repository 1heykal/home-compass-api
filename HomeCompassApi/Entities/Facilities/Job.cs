﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeCompassApi.Entities.Facilities
{
    public class Job
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int CategoryId { get; set; } = 0;

        [ForeignKey(nameof(CategoryId)), JsonIgnore]
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
        public string ContributorId { get; set; } = string.Empty;

        [ForeignKey(nameof(ContributorId)), JsonIgnore]
        public ApplicationUser Contributor { get; set; }
    }
}
