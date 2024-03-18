﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeCompassApi.Models.Cases
{
    public class Case
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }

        [DataType(DataType.ImageUrl)]
        public string PhotoUrl { get; set; }

        [Required]
        public bool Archived { get; set; } = false;
        public string AdditionalDetails { get; set; }

        public string ReporterId { get; set; }

        [ForeignKey(nameof(ReporterId))]
        [JsonIgnore]
        public ApplicationUser Reporter { get; set; }

    }
}
