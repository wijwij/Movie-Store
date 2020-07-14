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
     */
    public class MoviesStoreDbContext : DbContext
    {
        public MoviesStoreDbContext(DbContextOptions<MoviesStoreDbContext> options) : base(options)
        {
            
        }
        // multiple dbset as properties. Each DbSet => Table
        public DbSet<Genre> Genres { get; set; }
        
    }
}