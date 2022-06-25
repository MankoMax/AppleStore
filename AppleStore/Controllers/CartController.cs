using AppleStore.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AppleStore.Domain.ViewModels;
using AppleStore.DAL.Interfaces;

namespace AppleStore.Controllers
{
    public class CartController : Controller
    {
        private IBaseRepository<Product> _productRepository;

        public CartController(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult Index()
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart()
            });
        }

        public IActionResult AddToCart(int productId)
        {
            var product = _productRepository.GetAll()
                .FirstOrDefault(x => x.Id == productId);

            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var product = _productRepository.GetAll()
                .FirstOrDefault(g => g.Id == productId);

            if (product != null)
            {
                GetCart().RemoveLine(product);
            }

            return RedirectToAction("Index");
        }

        public Cart GetCart()
        {
            return new Cart();
        }

        /*        public Cart GetCart()
                {
                    Cart cart = (Cart)Session["Cart"];

                    if (cart == null)
                    {
                        cart = new Cart();
                        Session["Cart"] = cart;
                    }
                    return cart;
                }
        */
        /*  public async Cart GetCart()
          {
              var cart = new Cart();

              var key = string.Empty;

              await _session.LoadAsync();

              var bytes = _session.Get(key);

              _session.Set("Cart", bytes);

              return cart;

          }*/
    }
}
