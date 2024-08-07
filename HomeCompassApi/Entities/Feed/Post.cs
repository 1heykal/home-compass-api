﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeCompassApi.Entities.Feed
{
    public class Post
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        [Required] public string Content { get; set; }

        [DataType(DataType.DateTime)] public DateTime PublisedOn { get; set; }

        public bool Archived { get; set; } = false;
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public ApplicationUser User { get; set; }

        [JsonIgnore] public List<Comment> Comments { get; set; }
        [JsonIgnore] public List<Like> Likes { get; set; }
    }
}