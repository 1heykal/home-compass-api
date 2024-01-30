using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        // Cases
        public virtual DbSet<Homeless> Homeless { get; set; }
        public virtual DbSet<Missing> Missings { get; set; }

        // Facilities
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Category> Categories { get; set; }


        // Feed 
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
