using System.Threading.Tasks;

namespace Herald.EntityFramework.Repositories
{
    public interface IRepository<TEntity> : IReadonlyRepository<TEntity> where TEntity : class
    {
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
