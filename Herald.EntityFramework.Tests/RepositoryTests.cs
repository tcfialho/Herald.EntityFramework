
using System.Linq;
using System.Threading.Tasks;

using Herald.EntityFramework.Persistance;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace Herald.EntityFramework.Tests
{
    public class RepositoryTests
    {
        private IStubRepository stubRepository;
        private EntityContext entityContext;

        public RepositoryTests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<EntityContext>(options => 
            {
                options.UseLazyLoadingProxies();
                options.UseInMemoryDatabase("RepositoryTests");
            });
            services.AddHeraldEntityFramework<EntityContext>();
            services.AddTransient<IStubRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            var serviceProvider = services.BuildServiceProvider();
            stubRepository = serviceProvider.GetService<IStubRepository>();
            entityContext = serviceProvider.GetService<EntityContext>();
        }

        [Fact]
        public async Task ShouldInsert()
        {
            //Arrange
            var stub = new EntityStub("Stub");

            //Act
            await stubRepository.Insert(stub);
            entityContext.SaveChanges();

            //Assert            
            Assert.NotNull(entityContext.EntityStubs.Find(stub.Id));
        }

        [Fact]
        public void ShouldUpdate()
        {
            //Arrange
            var stub = new EntityStub("Stub");
            entityContext.EntityStubs.Add(stub);
            entityContext.SaveChanges();
            var stubToUpdate = entityContext.EntityStubs.Find(stub.Id);

            //Act
            stubToUpdate.Name = "updated";
            stubRepository.Update(stubToUpdate);
            entityContext.SaveChanges();

            //Assert            
            Assert.Equal("updated", entityContext.EntityStubs.Find(stub.Id).Name);
        }

        [Fact]
        public void ShouldDelete()
        {
            //Arrange
            var stub = new EntityStub("Stub");
            entityContext.EntityStubs.Add(stub);
            entityContext.SaveChanges();

            //Act
            stubRepository.Delete(stub);
            entityContext.SaveChanges();

            //Assert            
            Assert.Null(entityContext.EntityStubs.Find(stub.Id));
        }

        [Fact]
        public void ShouldGetById()
        {
            //Arrange
            var stub = new EntityStub("Stub");
            entityContext.EntityStubs.Add(stub);
            entityContext.SaveChanges();

            //Act
            var foundStub = stubRepository.GetById(stub.Id);

            //Assert            
            Assert.NotNull(foundStub);
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            //Arrange
            entityContext.EntityStubs.Add(new EntityStub("StubA"));
            entityContext.EntityStubs.Add(new EntityStub("StubB"));
            entityContext.SaveChanges();

            //Act
            var stubs = await stubRepository.GetAll();

            //Assert            
            Assert.NotEmpty(stubs);
        }
    }
}
