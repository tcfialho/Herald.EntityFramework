using Microsoft.EntityFrameworkCore;

namespace Herald.EntityFramework.Tests
{
    public class EntityContext : DbContext
    {
        public DbSet<EntityStub> EntityStubs { get; set; }

        public EntityContext()
        {
        }

        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
