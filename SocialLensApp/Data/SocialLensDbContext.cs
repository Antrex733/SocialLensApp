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

            modelBuilder.Entity<Comment>()
            .HasMany(c => c.ReplyList) // ReplyList to lista odpowiedzi
            .WithOne(c => c.ParentComment) // brak właściwości nawigacyjnej do rodzica
            .HasForeignKey(c => c.ParentCommentId) // ParentCommentId wskazuje rodzica
            .OnDelete(DeleteBehavior.Restrict); // NIE USUWA automatycznie odpowiedzi

            modelBuilder.Entity<Comment>()
                .HasOne<Post>() // Komentarz należy do posta
                .WithMany(p => p.CommentList) // Wiele komentarzy w poście
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict); // Usuwanie komentarzy razem z postem
        }
    }
}
