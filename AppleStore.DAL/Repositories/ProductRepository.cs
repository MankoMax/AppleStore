using AppleStore.DAL.Interfaces;
using AppleStore.Domain.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DAL.Repositories
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly ApplicationDbContext _db;

        // public delegate void ExecuteOperationWithSaveChanges();

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task ToExecuteOperationWithSaveChanges(Func<Task> executeOperationWithSaveChanges)
        {
            await executeOperationWithSaveChanges();

            await _db.SaveChangesAsync();
        }

        public async Task Create(Product entity)
        {
            Func<Task> action = async() =>
            {
               await _db.Products.AddAsync(entity);
            };

            await ToExecuteOperationWithSaveChanges(action);
        }

        public async Task Delete(Product entity)
        {
            Func<Task> action = async() =>
            {
                _db.Products.Remove(entity);
            };

            await ToExecuteOperationWithSaveChanges(action);
        }

        public IQueryable<Product> GetAll()
        {
            return _db.Products;
        }
    }
}
