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
    public class CategoriesController : Controller
    {
        #region  Constractor Region

        private readonly ICategoriesRepo _categoryRepo;
        private readonly IProductsRepo _productsRepo;
        public CategoriesController(ICategoriesRepo categoriesRepo, IProductsRepo productsRepo)
        {
            _categoryRepo = categoriesRepo;
            _productsRepo = productsRepo;
        }

        #endregion

        #region  Get All Categories Region

        public IActionResult AllCategoryRegion()
        {
            var allCategory = _categoryRepo.GetAll();
            ViewBag.allCategory2 = allCategory;
            return View();
        }

        #endregion

        #region  Get All Region

        public IActionResult GetAll()
        {
            // Get all products from all categories
            var allProducts = _productsRepo.GetAll();
            return View(allProducts);
        }

        #endregion

        #region Get Products For Category Region

        // GET Categories/Products/{categoryId}
        public IActionResult GetProductsForCategory(int categoryId)
        {
            var products = _categoryRepo.GetProductsForCategory(categoryId);
            return View(products);
        }

        #endregion

        #region Create GET Region

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region Create Post Region

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name,Description,Icon")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Insert(category);
                return RedirectToAction(nameof(AllCategoryRegion), "Categories");
            }
            return RedirectToAction(nameof(AllCategoryRegion), "AllCategoryRegion", category);
        }

        #endregion

        #region Edit
     
        #region Edit Get Region

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();
            var category = _categoryRepo.GetById(id);
            if (category == null) return NotFound();
            ViewBag.AllcategoryNames = _categoryRepo.GetAll();

            return View(category);
        }

        #endregion

        #region Post Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try { _categoryRepo.Update(category); }

                catch (DbUpdateConcurrencyException)
                { if (category.Id == 0) return NotFound(); else throw; }

                return RedirectToAction(nameof(CategoriesController.AllCategoryRegion), "Categories");
            }
            return View(category);
        }

        #endregion

        #endregion

        #region delete 

        #region Get delete 

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();
            var category = _categoryRepo.GetById(id);
            if (category == null) return NotFound();
            return View(category);

        }

        #endregion

        #region Post delete 

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = _categoryRepo.GetById(id);
            if (category != null)
            {
                _categoryRepo.Delete(category);
            }

            return RedirectToAction(nameof(AllCategoryRegion), "Categories");
        }

        #endregion


        #endregion

    }
}
