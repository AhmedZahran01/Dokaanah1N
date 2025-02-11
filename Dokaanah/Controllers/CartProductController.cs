using Dokaanah.Models;
using Dokaanah.PresentationLayer;
using Dokaanah.PresentationLayer.ViewModels;
using Dokaanah.Repositories.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Dokaanah.Controllers
{
    public class CartProductController : Controller
    {
        #region  Constractor Region
        private readonly ICartProductRepo _cartproductRepo;
        private readonly IProductsRepo _productRepo;

        public CartProductController(ICartProductRepo cartproductRepo, IProductsRepo productsRepo)
        {
            _cartproductRepo = cartproductRepo;
            _productRepo = productsRepo;
        }

        #endregion


        #region Add Product To Cart to shop

        public IActionResult AddProductToCarttoshop(int Id)
        { 
            //var prd = dbcontext.Products.FirstOrDefault(x => x.Id == Id);
            var prd = _productRepo.GetById(Id);


            var cartitems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var existingcartitem = cartitems.FirstOrDefault(item => item.Product.Id == Id);
            if (existingcartitem != null)
            {
                existingcartitem.Quantity++;
            }
            else
            {
                cartitems.Add(new ShoppingCartItem
                {
                    Product = prd,
                    Quantity = 1
                });
            }
            HttpContext.Session.Set("Cart", cartitems);
            return RedirectToAction("shop", "home");
        }


        #endregion


        #region Add Product To cart by Home

        public IActionResult AddProductTocartbyHome(int Id)
        { 
            //var prd = dbcontext.Products.FirstOrDefault(x => x.Id == Id);
            var prd = _productRepo.GetById(Id);

            var cartitems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var existingcartitem = cartitems.FirstOrDefault(item => item.Product.Id == Id);
            if (existingcartitem != null)
            {
                existingcartitem.Quantity++;
            }
            else
            {
                cartitems.Add(new ShoppingCartItem
                {
                    Product = prd,
                    Quantity = 1
                });
            }
            HttpContext.Session.Set("Cart", cartitems);
            return RedirectToAction("index", "home");
        }


        #endregion


        #region View Cart Region
       
        [HttpGet]
        public IActionResult ViewCart()
        {
            var cartitems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var cartitemviewmodel = new shoppingCartViewModel
            {
                CartItems = cartitems,
                TotalPrice = cartitems.Sum(item => item.Product.Price * item.Quantity),
                TotalQuantity = cartitems.Sum(item => item.Quantity)
            };

            return View(cartitemviewmodel);
        }

        #endregion


        #region Checkout Region

        [Authorize(Roles = "Admin")]
        public IActionResult Checkout()
        {
            var cartitems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var cartitemviewmodel = new shoppingCartViewModel
            {
                CartItems = cartitems,
                TotalPrice = cartitems.Sum(item => item.Product.Price * item.Quantity)
            };
            ViewBag.totalprice = cartitemviewmodel.TotalPrice;
            return View(cartitemviewmodel);
        }


        #endregion


        #region Remove Product From Cart Region

        [HttpPost]
        public IActionResult RemoveProductFromCart(int productId)
        {
            var cartitems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart");
            var itemToRemove = cartitems?.FirstOrDefault(item => item.Product.Id == productId);

            if (itemToRemove != null)
            {
                cartitems.Remove(itemToRemove);
                HttpContext.Session.Set("Cart", cartitems);
            }

            return RedirectToAction("ViewCart");
        }
        #endregion

     
        #region  Update Cart Product Quantity Region

        [HttpPost]
        public IActionResult UpdateCartProductQuantity(int productId, int quantity)
        {
            var cartitems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart");
            var cartItem = cartitems?.FirstOrDefault(item => item.Product.Id == productId);

            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    cartitems.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                }
                HttpContext.Session.Set("Cart", cartitems);
            }

            return Ok();
        }

        #endregion


        #region payment Action

        [Authorize(Roles = "Admin")]
        public IActionResult paymentAction()
        {
            var cartitems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var cartitemviewmodel = new shoppingCartViewModel
            {
                CartItems = cartitems,
                TotalPrice = cartitems.Sum(item => item.Product.Price * item.Quantity)
            };
            ViewBag.totalprice = cartitemviewmodel.TotalPrice;
            return View();
        }
        #endregion


        #region payment Succes Region

        [Authorize(Roles = "Admin")]
        public IActionResult paymentSucces()
        {
            return View();
        }

        #endregion

    }
}
