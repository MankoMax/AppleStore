using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);

        IQueryable<T> GetAll();

        Task Delete(T entity);
    }
}
