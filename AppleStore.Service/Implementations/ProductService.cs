using AppleStore.DAL.Interfaces;
using AppleStore.Domain.Entity;
using AppleStore.Domain.Enum;
using AppleStore.Domain.Response;
using AppleStore.Domain.ViewModels.Product;
using AppleStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;

        public ProductService(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IBaseResponse<Product>> Create(ProductViewModel model)
        {
            try
            {
                var myImageDict = new Dictionary<string, string>
            {
                { "iPhone 6", "https://support.apple.com/library/APPLE/APPLECARE_ALLGEOS/SP705/SP705-iphone_6-mul.png" },
                { "iPhone 7", "https://bigmag.ua/image/cache/catalog/archive/data/1c-iphone-7/black2year-650x540.png" },
                { "iPhone 8", "https://swipe.ua/content/images/23/335x390l50nn0/aktivirovannyy-apple-iphone-8-64gb-gold-mq6m2id-57658101470531.png" }
            };
                var productImage = myImageDict.GetValueOrDefault(model.Name);

                var product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Image = productImage,
                };

                await _productRepository.Create(product);

                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.OK,
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Product>> DeleteProduct(int id)
        {
            var baseResponse = new BaseResponse<Product>();

            try
            {
                var product = _productRepository.GetAll().FirstOrDefault(x => x.Id == id);

                if (product == null)
                {
                    baseResponse.StatusCode = StatusCode.InternalServerError;

                    return baseResponse;
                }

                await _productRepository.Delete(product);

                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Product>> GetProducts()
        {
            try
            {
                var products = _productRepository.GetAll().ToList();

                if (!products.Any())
                {
                    return new BaseResponse<List<Product>>()
                    {
                        Description = "No Elements",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Product>>()
                {
                    Data = products,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = $"[GetProducts] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
