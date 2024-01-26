﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeCompassApi.Models.Feed;

namespace HomeCompassApi.Models
{
    public class ApplicationUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.ImageUrl)]
        public Uri PhotoUrl { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Range(minimum: 17, maximum: 100)]
        public int Age { get; set; }

        public string Gender { get; set; }

        public string Role { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
