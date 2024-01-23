using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeCompassApi.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublisedOn { get; set; }

        public bool Archived { get; set; } = false;

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
