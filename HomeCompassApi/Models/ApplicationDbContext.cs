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
        public virtual DbSet<Job> Jobs { get; set; }


        // Feed 
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Report> Reports { get; set; }

        // Info
        public virtual DbSet<Info> Info { get; set; }


        private readonly IConfiguration _configuration;

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Job>().Property<decimal>(j => j.Salary).HasPrecision(38, 18);
            //builder.Entity<ApplicationUser>().HasQueryFilter(u => !u.IsDeleted);


            builder.Entity<Report>()
                .HasOne(r => r.Reporter)
                .WithMany(u => u.Reports)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(builder);
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
