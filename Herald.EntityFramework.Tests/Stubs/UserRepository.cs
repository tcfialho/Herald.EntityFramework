using System.Linq;

using Herald.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Herald.EntityFramework.Tests
{
    public class UserRepository : Repository<EntityStub>, IStubRepository
    {
        protected override IQueryable<EntityStub> _query { get; set; }

        public UserRepository(DbContext context) : base(context)
        {
        }        
    }
}