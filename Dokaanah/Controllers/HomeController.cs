using Dokaanah.Models;
using Dokaanah.Repositories.RepoClasses;
using Dokaanah.Repositories.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Dokaanah.Controllers
{
    public class HomeController : Controller
    {
        #region  Constractor Region
       
        private readonly ICategoriesRepo _categoriesRepo;
        private readonly IProductsRepo _productsRepo;
        private readonly UserManager<Customer> _userManager;
        private readonly ICustomersRepo _customersRepo; 
        public HomeController(  ICategoriesRepo categoriesRepo, IProductsRepo productsRepo , 
                                   UserManager<Customer> userManager , ICustomersRepo customersRepo)
        {
            _categoriesRepo = categoriesRepo;
            _productsRepo   = productsRepo;
            _userManager    = userManager;
            _customersRepo  = customersRepo;
        }

        #endregion

        #region Index Region
        
        public IActionResult Index()
        {  
            return View(_productsRepo.GetRandomProducts(5));
        }
        #endregion

        #region Shop Region
     
        public IActionResult Shop(string categoryName , int categoryNumber, string productName , int CategoryId)
        {

            if (string.IsNullOrEmpty(productName) && CategoryId == 0)
            { 
                var allproducts = _productsRepo.GetProductsByCategoryId(categoryNumber);
                var allcategories = _categoriesRepo.GetAll().ToList();
                ViewBag.cats = allcategories;
                categoryName = string.IsNullOrEmpty(categoryName) ? "all" : categoryName;
                ViewData["catName"] = categoryName;
                if(categoryNumber !=0)
                { 
                    ViewBag.allproductsHere = _productsRepo.GetAll().Where(p => p.CategoryId == categoryNumber).ToList();
                    return View(allproducts);
                }
                ViewBag.allproductsHere = _productsRepo.GetAll().ToList();
                return View(allproducts);
            }
              
            else if (string.IsNullOrEmpty(productName) && CategoryId != 0)
            {
                var allproductsWithCats = _productsRepo.GetAll();
                var allcategories = _categoriesRepo.GetAll().ToList();
                ViewBag.cats = allcategories;
                categoryName = string.IsNullOrEmpty(categoryName) ? "all" : categoryName;
                ViewData["catName"] = categoryName;

                ViewBag.allproductsHere = allproductsWithCats.Where(p => p.CategoryId == CategoryId).ToList();
                return View(allproductsWithCats);
            }
             
            else
            {
                var allproductsWithCats = _productsRepo.GetAll();
                var allcategories = _categoriesRepo.GetAll().ToList();
                ViewBag.cats = allcategories;
                categoryName = string.IsNullOrEmpty(categoryName) ? "all" : categoryName;
                ViewData["catName"] = categoryName;

                ViewBag.allproductsHere = _productsRepo.SearchByName(productName,CategoryId).ToList();
                return View(allproductsWithCats);
            }


        }

        #endregion

        #region About Us Region
        
        public IActionResult AboutUs()
        {
            return View();
        }
        #endregion

        #region Contant Us Region

        public IActionResult ContantUs()
        {
            return View();
        }

        #endregion

        #region User Account Region

        [Authorize]
        public async Task<IActionResult> UserAccount()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user =   _customersRepo.GetByUserByEmail(email); 

            //ViewBag.CurrentUserData = user;
            return View(user);
        }

        #endregion
         
        #region Error Region
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Error Handle For User Region

        public IActionResult ErrorHandleForUser()
        {
            return View();
        }

        #endregion

        #region FAQ Region
      
        public IActionResult FAQ()
        {
            return View();
        }

        #endregion

    }
}
