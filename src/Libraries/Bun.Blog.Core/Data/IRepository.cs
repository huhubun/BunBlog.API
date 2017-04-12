using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.Core.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);

        T Add(T entity);

        Task<T> AddAsync(T entity);

        T Update(T entity);

        Task<T> UpdateAsync(T entity);

        int Delete(T entity);

        Task<int> DeleteAsync(T entity);

        IQueryable<T> Table { get; }
    }
}
