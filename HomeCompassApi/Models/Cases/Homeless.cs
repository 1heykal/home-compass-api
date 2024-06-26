﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeCompassApi.Models.Cases
{
    public class Homeless : Case
    {
        public string CurrentLocation { get; set; }
        public string HealthCondition { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public List<string> Skills { get; set; }

    }
}
