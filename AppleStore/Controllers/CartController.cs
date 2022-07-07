using AppleStore.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AppleStore.DAL.Infrastructure;
using AppleStore.Domain.ViewModels;
using AppleStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppleStore.Service.Interfaces;
using System.Text.Json.Serialization;
using System;

namespace AppleStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IBaseRepository<Product> _productRepository;

        public CartController(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
                var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();

                CartViewModel cartVM = new()
                {
                    CartItems = cart,
                    GrandTotal = cart.Sum(x => x.Quantity * x.Price)
                };

                return View(cartVM);
        }

        public IActionResult Add(long id)
        {
            var product = _productRepository.GetAll().FirstOrDefault(x => x.Id == id);

            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();

            var cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.Set("Cart", cart);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Decrease(long id)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");

            var cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(long id)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }
    }

}
