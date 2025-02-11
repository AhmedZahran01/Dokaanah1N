using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dokaanah.Models;
using Dokaanah.Repositories.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Dokaanah.Repositories.RepoClasses;

namespace Dokaanah.Controllers
{
    //Product_Category
    public class Product_CategoryController : Controller
    {
        #region  Constractor Region
         
        private readonly IProductsRepo productsRepo;
        public Product_CategoryController(IProductsRepo productsRepo )
        {
            this.productsRepo = productsRepo;
        }

        #endregion

        #region Index Region
 
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var dokkanah2Contex = productsRepo.GetAll();
            return View(dokkanah2Contex.ToList());
        }
        #endregion

        #region Get All Product By Category Region
       
        public async Task<IActionResult> GetallproductBycategory(int id)
        {
            var AllProductForSpecificCategory = productsRepo.GetAll().Where(c => c.CategoryId == id).ToList();
            return View(AllProductForSpecificCategory);
        }
             
        #endregion
    }
}

#region   Comment Region


          //    var vv = productsRepo.GetAll().Where(c => c.CategoryId == id).ToList();
          
          //    var allproduct = new List<Product>();
          //            foreach (var item in vv)
          //            {
          //                allproduct.Add(item);
          //            }
          //return View(allproduct);
#endregion





