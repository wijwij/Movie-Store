using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;

namespace MovieStore.Infrastructure.Data
{
    /*
     * 1. Install all the EF Core libraries using NuGet
     * 2. Create class inherits from `DbContext` class, which represents your database.
     * 3. Create database connection string inside `appsettings.json` file
     * 4. Create an Entity/Domain Class, called Genre (Genre table)
     * 5. Add Entity class as a DbSet inside your DbContext Class
     * 6. Add reference for library that has DbContext to your startup project.
     * 7. Tell MVC project we are using EF in `startup.cs`
     * 8. Add DbContext options as constructor params
     * 9. Use Migration to create our database.
     *    `dotnet ef migrations add <MigrationName> --project <DbContext dir> --startup-project <dir of the entry point>`
     *     make sure your migration names are meaningful.
     *     why you need startup project? The tools have to execute the code to get info about the project, like database connection string.
     * 10. Then verifying it using update-database command. `dotnet ef database update --startup-project <dir>`
     *
     * IN EF we have 2 ways to create our entities and model our database using Code-First approach
     *   1. Data Annotations which is nothing but bunch of C# attributes that we can use
     *   2. Fluent API is more syntax and more powerful and usually uses lambdas
     * Combine both DataAnnotations and Fluent API
     */
    public class MoviesStoreDbContext : DbContext
    {
        public MoviesStoreDbContext(DbContextOptions<MoviesStoreDbContext> options) : base(options)
        {
            
        }
        // multiple dbset as properties. Each DbSet => Table
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent API configuration to model our tables
            modelBuilder.Entity<Movie>((modelBuilder) =>
            {
                modelBuilder.ToTable("Movie");
                modelBuilder.HasKey(m => m.Id);
                modelBuilder.Property(m => m.Title).IsRequired().HasMaxLength(256);
                modelBuilder.Property(m => m.Overview).HasMaxLength(4096);
                modelBuilder.Property(m => m.Tagline).HasMaxLength(512);
                modelBuilder.Property(m => m.ImdbUrl).HasMaxLength(2084);
                modelBuilder.Property(m => m.TmdbUrl).HasMaxLength(2084);
                modelBuilder.Property(m => m.PosterUrl).HasMaxLength(2084);
                modelBuilder.Property(m => m.BackdropUrl).HasMaxLength(2084);
                modelBuilder.Property(m => m.OriginalLanguage).HasMaxLength(64);
                modelBuilder.Property(m => m.Budget).HasColumnType("decimal(18, 2)");
                modelBuilder.Property(m => m.Revenue).HasColumnType("decimal(18, 2)");
                modelBuilder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
                modelBuilder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
                // ToDo do we need to configure datatime data type

                // we dont want to create rating column but we want c# rating property in out Entity so we can show rating in the UI
                modelBuilder.Ignore(m => m.Rating);
            });

            modelBuilder.Entity<Trailer>((modelBuilder) =>
            {
                modelBuilder.ToTable("Trailer");
                modelBuilder.HasKey(t => t.Id);
                modelBuilder.Property(t => t.Name).HasMaxLength(2084);
                modelBuilder.Property(t => t.TrailerUrl).HasMaxLength(2084);
                modelBuilder.HasOne(t => t.Movie).WithMany(m => m.Trailers).HasForeignKey(t => t.MovieId);
            });

            modelBuilder.Entity<MovieGenre>((modelBuilder) =>
            {
                modelBuilder.ToTable("MovieGenre");
                modelBuilder.HasKey(mg => new {mg.GenreId, mg.MovieId});
                modelBuilder.HasOne(mg => mg.Movie).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.MovieId);
                modelBuilder.HasOne(mg => mg.Genre).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.GenreId);
            });

            modelBuilder.Entity<Role>(modelBuilder =>
            {
                modelBuilder.ToTable("Role");
                modelBuilder.HasKey(r => r.Id);
                modelBuilder.Property(r => r.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<UserRole>(modelBuilder =>
            {
                modelBuilder.ToTable("UserRole");
                modelBuilder.HasKey(ur => new {ur.UserId, ur.RoleId});
                modelBuilder.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
                modelBuilder.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
            });

            modelBuilder.Entity<Favorite>(modelBuilder =>
            {
                modelBuilder.ToTable("Favorite");
                modelBuilder.HasKey(f => f.Id);
                modelBuilder.HasOne(f => f.User).WithMany(u => u.Favorites).HasForeignKey(f => f.UserId);
                modelBuilder.HasOne(f => f.Movie).WithMany(m => m.Favorites).HasForeignKey(f => f.MovieId);
            });

            modelBuilder.Entity<Review>(modelBuilder =>
            {
                modelBuilder.ToTable("Review");
                modelBuilder.HasKey(r => new {r.MovieId, r.UserId});
                modelBuilder.Property(r => r.Rating).HasColumnType("decimal(3, 2)").IsRequired();
                modelBuilder.HasOne(r => r.Movie).WithMany(m => m.Reviews).HasForeignKey(r => r.MovieId);
                modelBuilder.HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId);
            });

            modelBuilder.Entity<Cast>(modelBuilder =>
            {
                modelBuilder.ToTable("Cast");
                modelBuilder.HasKey(c => c.Id);
                modelBuilder.Property(c => c.Name).HasMaxLength(128);
                modelBuilder.Property(c => c.ProfilePath).HasMaxLength(2048);
            });

            modelBuilder.Entity<MovieCast>(modelBuilder =>
            {
                modelBuilder.ToTable("MovieCast");
                modelBuilder.HasKey(mc => new {mc.CastId, mc.MovieId});
                modelBuilder.HasOne(mc => mc.Cast).WithMany(c => c.MovieCasts).HasForeignKey(mc => mc.CastId);
                modelBuilder.HasOne(mc => mc.Movie).WithMany(m => m.MovieCasts).HasForeignKey(mc => mc.MovieId);
            });

            modelBuilder.Entity<User>(modelBuilder =>
            {
                modelBuilder.ToTable("User");
                modelBuilder.HasKey(u => u.Id);
                modelBuilder.Property(u => u.UserName).HasMaxLength(256);
                modelBuilder.Property(u => u.Email).HasMaxLength(256);
                // bit data type
                modelBuilder.Property(u => u.TwoFactorEnabled).HasColumnType("bit");
            });

            modelBuilder.Entity<Purchase>(modelBuilder =>
            {
                modelBuilder.ToTable("Purchase");
                modelBuilder.HasKey(p => p.Id);
                modelBuilder.Property(p => p.PurchaseNumber).HasColumnType("UNIQUEIDENTIFIER");
                modelBuilder.Property(p => p.TotalPrice).IsRequired().HasColumnType("decimal(5, 2)");
                modelBuilder.HasOne(p => p.Movie).WithMany(m => m.Purchases).HasForeignKey(p => p.MovieId);
                modelBuilder.HasOne(p => p.Customer).WithMany(u => u.Purchases).HasForeignKey(p => p.UserId);
            });
        }
    }
}