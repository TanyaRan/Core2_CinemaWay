namespace CinemaWay.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CinemaWayDbContext : IdentityDbContext<User>
    {
        public CinemaWayDbContext(DbContextOptions<CinemaWayDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Projection> Projections { get; set; }

        public DbSet<Theme> Themes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Story> Stories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Comment>()
                .HasOne(c => c.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MovieId);

            builder
                .Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AuthorId);

            builder
                .Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            builder
                .Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.Movies)
                .HasForeignKey(ma => ma.ActorId);

            builder
                .Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.Actors)
                .HasForeignKey(ma => ma.MovieId);

            builder
                .Entity<Projection>()
                .HasOne(p => p.Movie)
                .WithMany(m => m.Projections)
                .HasForeignKey(p => p.MovieId);

            builder
                .Entity<Projection>()
                .HasOne(p => p.Theme)
                .WithMany(t => t.Projections)
                .HasForeignKey(p => p.ThemeId);

            builder
                .Entity<Projection>()
                .HasOne(p => p.Lecturer)
                .WithMany(l => l.Lectures)
                .HasForeignKey(p => p.LecturerId);

            builder
                .Entity<UserProjections>()
                .HasKey(up => new { up.ProjectionId, up.VisitorId });

            builder
                .Entity<UserProjections>()
                .HasOne(up => up.Projection)
                .WithMany(p => p.Visitors)
                .HasForeignKey(up => up.ProjectionId);

            builder
                .Entity<UserProjections>()
                .HasOne(up => up.Visitor)
                .WithMany(v => v.Tickets)
                .HasForeignKey(up => up.VisitorId);

            builder
                .Entity<Story>()
                .HasOne(a => a.Author)
                .WithMany(u => u.Stories)
                .HasForeignKey(a => a.AuthorId);

            base.OnModelCreating(builder);
        }
    }
}
