using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
