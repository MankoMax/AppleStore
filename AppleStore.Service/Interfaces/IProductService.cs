using AppleStore.Domain.Entity;
using AppleStore.Domain.Response;
using AppleStore.Domain.ViewModels.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppleStore.Service.Interfaces
{
    public interface IProductService
    {
        IBaseResponse<List<Product>> GetProducts();

        Task<IBaseResponse<Product>> Create(ProductViewModel product);

        Task<IBaseResponse<Product>> DeleteProduct(int id);
    }
}
