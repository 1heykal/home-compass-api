﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeCompassApi.Models.Facilities
{
    public class Category
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [JsonIgnore]
        public List<Facility> Facilities { get; set; }

        [JsonIgnore]
        public List<Job> Jobs { get; set; }

    }
}
