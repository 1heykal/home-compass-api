using HomeCompassApi.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        // public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("SqlServer");
                optionsBuilder.UseSqlServer(connectionString);
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
