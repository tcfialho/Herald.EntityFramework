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
    }
}
