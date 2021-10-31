using Herald.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Herald.EntityFramework.Tests
{
    public class UserRepository : Repository<EntityStub>, IStubRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}