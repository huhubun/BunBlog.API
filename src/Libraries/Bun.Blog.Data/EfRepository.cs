using Bun.Blog.Core.Data;
using Bun.Blog.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public EfRepository(BlogContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Table => _dbSet;

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T Add(T entity)
        {
            entity.SetInsertAndUpdateDate();

            _dbSet.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.SetInsertAndUpdateDate();

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public T Update(T entity)
        {
            entity.SetUpdateDate();

            _context.SaveChanges();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.SetUpdateDate();

            await _context.SaveChangesAsync();

            return entity;
        }

        public int Delete(T entity)
        {
            _dbSet.Remove(entity);

            return _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            return await _context.SaveChangesAsync();
        }
    }
}
