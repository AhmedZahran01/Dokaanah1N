using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dokaanah.Models;
using Dokaanah.Repositories.RepoInterfaces;
using Dokaanah.Repositories.RepoClasses;
 using Microsoft.AspNetCore.Authorization;

namespace Dokaanah.Controllers
{
    public class ProductController : Controller
    {
        #region  Constractor Region
  
        private readonly IProductsRepo _productRepo;
        private readonly ICategoriesRepo _categoryRepo;
        public ProductController(IProductsRepo productRepo, ICategoriesRepo categoryRepo )
        { 
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }
           
        #endregion

        #region List Region

        [Authorize]
        public IActionResult list()
        { 
             return View(_productRepo.GetAll());
        }
        #endregion

        #region Details Region
        
        public IActionResult Details(int id)
        {
            var product = _productRepo.GetById(id);
            if (product == null)  return NotFound(); 
            return View(product);
        }

        #endregion

        #region Random Products Region
        
        public IActionResult RandomProducts()
        { 
            return View(_productRepo.GetRandomProducts(4));
        }

        #endregion

        #region Add To Cart Region
       
        [HttpPost]
        public IActionResult AddToCart(int productId, int cartId)
        {
            _productRepo.AddProductToCart(productId, cartId);
            return View();
        }

        #endregion

        #region Shop Region

        public IActionResult Shop(string category)
        {
            // Fetch products based on the selected category
            var products = _productRepo.GetAll()
                .Where(p => category == "All" );

            // Pass the filtered products to the view
            return View(products);
        } 

        #endregion
         
        #region  Manage CRED Operations product By Admin
      
        #region create http post

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name,Price,Description,ImgUrl,Quantity,CategoryId")] Product product )
        {
            if (ModelState.IsValid)
            {
                _productRepo.Insert(product);  
                return RedirectToAction(nameof(AdminDashboardController.Index), "AdminDashboard");
            }
            
            return View(product);
        }

        #endregion

        #region Edit

        #region Get Edit

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();
            var product = _productRepo.GetById(id);
            if (product == null) return NotFound();
            ViewBag.AllcategoryNames = _categoryRepo.GetAll();
            return View(product);
        }

        #endregion
         
        #region Post Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit([Bind("Id,Name,Price,Description,ImgUrl,Quantity,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                try { _productRepo.Update(product); }
                catch (DbUpdateConcurrencyException)
                { if (product.Id == 0) return NotFound(); else throw; }
                return RedirectToAction(nameof(AdminDashboardController.Index), "AdminDashboard");
            }

            return View(product);
        }

        #endregion

        #endregion

        #region delete 

        #region Get delete 

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();
            var product = _productRepo.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        #endregion
       
        #region Post delete 

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = _productRepo.GetById(id);
            if (product != null) _productRepo.Delete(product);
            return RedirectToAction(nameof(Index), "AdminDashboard");
        }

        #endregion
         
        #endregion
         
        #endregion

    }
}


#region Comment Region


// POST: Products/Edit/5
// To protect from overposting attacks, enable the specific properties you want to bind to.
// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


//var categoryNames = _categoryRepo.GetAll().Select(c => c.Name).ToList();
//ViewBag.CategoryNames = categoryNames;

//ViewBag.productDatails = product;


// GET: Product/Random




//// GET: Product/Create
//public IActionResult Create()
//{
//    return View();
//}

// POST: Products/Create
// To protect from overposting attacks, enable the specific properties you want to bind to.
// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

#endregion