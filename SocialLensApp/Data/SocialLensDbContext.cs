using Microsoft.EntityFrameworkCore;
using SocialLensApp.Entities;

namespace SocialLensApp.Data
{
    public class SocialLensDbContext :DbContext
    {
        public SocialLensDbContext(DbContextOptions<SocialLensDbContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post>  Posts  { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Invite> Invites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
