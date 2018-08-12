using Microsoft.EntityFrameworkCore;
namespace moviesList.DbEntities
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        //DbSet
        public DbSet<Movies> Movies { get; set; } 
        public DbSet<Actors> Actors { get; set; }
        public DbSet<Producers> Producers { get; set; }
        public DbSet<MovieActorMapping> movieActorMappings { get; set; }
        public DbSet<MovieProducerMapping> movieProducerMappings { get; set; }

        //ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {  
            base.OnModelCreating(modelBuilder);

        }  
    }
}
