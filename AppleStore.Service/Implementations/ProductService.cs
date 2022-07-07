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
                { "iPhone 6", "https://htstatic.imgsmail.ru/pic_image/fce72fb5490493da10584d6add4d49e4/450/450/1334887/" },
                { "iPhone 7", "https://bigmag.ua/image/cache/catalog/archive/data/1c-iphone-7/black2year-650x540.png" },
                { "iPhone 8", "https://a-mad.ru/wp-content/uploads/2017/10/iphone-8plus-spacegray.jpg" },
                { "iPhone X", "https://zhzh.info/_pu/107/83911675.jpg" },
                { "iPhone XS", "https://assets.swappie.com/cdn-cgi/image/width=600,height=600,fit=contain,format=auto/swappie-iphone-xs-max-gold.png" },
                { "iPhone 11", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQyIadKIf7W7o6Dcj8oKKPOXvR0v2KUgwcpGoDllJKPNvsLLtakZQrAN-pxfZoL1EBCA4I&usqp=CAU" },
                { "iPhone 12", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0EqK7iHACiRMkP29XGjSgGb4eCLeH6sL-7e3-53fL-yAw9Px6V_BhP_3eKJNyVn16iCo&usqp=CAU" },
                { "iPhone 13", "https://cdn1.it4profit.com/AfrOrF3gWeDA6VOlDG4TzxMv39O7MXnF4CXpKUwGqRM/resize:fill:540/bg:f6f6f6/q:100/plain/s3://catalog-products/210915083530651364/210927160010235057.png@webp" },
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
