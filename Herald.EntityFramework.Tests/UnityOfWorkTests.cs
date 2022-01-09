
using System;
using System.Linq;
using System.Threading.Tasks;

using Herald.EntityFramework.Persistance;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace Herald.EntityFramework.Tests
{
    public class UnityOfWorkTests
    {
        private IStubRepository stubRepository;
        private IUnitOfWork unitOfWork;
        private EntityContext entityContext;

        public UnityOfWorkTests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<EntityContext>(options => options.UseInMemoryDatabase("RepositoryTests"));
            services.AddHeraldEntityFramework<EntityContext>();
            services.AddTransient<IStubRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            var serviceProvider = services.BuildServiceProvider();
            stubRepository = serviceProvider.GetService<IStubRepository>();
            unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            entityContext = serviceProvider.GetService<EntityContext>();
        }

        [Fact]
        public async Task ShouldSaveChanges()
        {
            //Arrange
            var stub = new EntityStub("Stub");

            //Act
            await stubRepository.Insert(stub);
            await unitOfWork.Commit();

            //Assert            
            Assert.Equal(EntityState.Unchanged, entityContext.Entry(stub).State);
        }

        [Fact]
        public async Task ShouldNotSaveChanges()
        {
            //Arrange
            var stub = new EntityStub("Stub");

            //Act
            await stubRepository.Insert(stub);

            
            //Assert            
            Assert.Equal(EntityState.Added, entityContext.Entry(stub).State);
        }
    }
}
