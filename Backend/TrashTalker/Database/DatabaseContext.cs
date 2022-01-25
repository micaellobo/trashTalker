using Microsoft.EntityFrameworkCore;
using TrashTalker.Models;

namespace TrashTalker.Database
{
    /// <summary>
    /// This class represents a database instance and can be used to query and save instances of your entities.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Constructor method of the database instance. 
        /// </summary>
        /// <param name="options">The options to be used by a DbContext.</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// This method DbSet of type User can be used to query and save instances of User.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// This method DbSet of type Container can be used to query and save instances of Container.
        /// </summary>
        public DbSet<Container> Containers { get; set; }

        /// <summary>
        /// This method DbSet of type RecycleBin can be used to query and save instances of RecycleBin.
        /// </summary>
        public DbSet<RecycleBin> RecycleBins { get; set; }

        /// <summary>
        /// This method DbSet of type Picking can be used to query and save instances of Picking.
        /// </summary>
        public DbSet<Picking> Pickings { get; set; }

        /// <summary>
        /// This method DbSet of type Measurement can be used to query and save instances of Measurement.
        /// </summary>
        public DbSet<Measurement> Measurements { get; set; }

        /// <summary>
        /// This method DbSet of type Route can be used to query and save instances of Route.
        /// </summary>
        public DbSet<Route> Routes { get; set; }

        /// <summary>
        /// This method DbSet of type Alert can be used to query and save instances of Alert.
        /// </summary>
        public DbSet<Alert> Alerts { get; set; }

        /// <summary>
        /// This method DbSet of type Alert can be used to query and save instances of Alert.
        /// </summary>
        public DbSet<CollectPoint> CollectPoint { get; set; }

        /// <summary>
        /// This method creates a model that defines the shape of
        /// your entities, the relationships between them, and how they map to the database
        /// </summary>
        /// <param name="modelBuilder">Model for the context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>()
            //     .HasKey(user => new {user.id, user.username});

            // modelBuilder.Entity<Route>()
            //     .HasMany(left => left.recycleBins)
            //     .WithMany(right => right.routes)
            //     .UsingEntity(join => join.ToTable("CollectPoint"));
        }
    }
}