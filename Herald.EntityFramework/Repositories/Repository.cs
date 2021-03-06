using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Herald.EntityFramework.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        protected abstract IQueryable<TEntity> _query
        {
            get; set;
        }

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _query = _dbSet;
        }

        public virtual async Task<TEntity> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task Update(TEntity entity)
        {            
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;

            await Task.CompletedTask;
        }

        public virtual async Task Delete(TEntity entity)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

            await Task.CompletedTask;
        }

        public virtual async Task<IList<TEntity>> GetAll()
        {
            return await _query.ToListAsync();
        }
    }
}
