using Dokaanah.Models;
using Dokaanah.Repositories.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dokaanah.Controllers
{
    //AdminDashboard
    [Authorize(Roles="Admin")]
    public class AdminDashboardController : Controller
    {  

        #region Constractor Region 
         
        private readonly ICustomersRepo  customersRepo;
        private readonly ICategoriesRepo categoriesRepo;
        private readonly IProductsRepo   productsRepo;

        public AdminDashboardController(ICustomersRepo customersRepo,ICategoriesRepo categoriesRepo, IProductsRepo productsRepo)
        {
            this.customersRepo=customersRepo;
            this.categoriesRepo=categoriesRepo;
            this.productsRepo=productsRepo;
        }

        #endregion
         
        #region Index Region

        public IActionResult Index()
        {
            #region view bag data 
            ViewBag.customerCount = customersRepo.GetAll().Count();

            List<Product> AllproductsData = productsRepo.GetAll().ToList();
            ViewBag.Allproducts = AllproductsData;
            ViewBag.ProductCount = AllproductsData.Count();

            ViewBag.Allcategory = categoriesRepo.GetAll();

            #endregion


            return View();
        }

        #endregion
         
        #region View Login Logs Region
         
        public async Task<IActionResult> ViewCustomerLogs()
        {
            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "logs", "customer_activity.log");

            if (!System.IO.File.Exists(logPath))
            {
                System.IO.File.WriteAllText(logPath, ""); // Create the file if it doesn't exist
            }

            // Read the file with shared access
            List<string> logs;
            using (var stream = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                logs = new List<string>();
                while (!reader.EndOfStream)
                {
                    logs.Add(await reader.ReadLineAsync());
                }
            }

            return View(logs);
        }
 
        #endregion

    }
     

    #region Comment Class Region


    #region ViewLoginLogs1 Region

    #region ViewLoginLogs1 Region

    //public IActionResult ViewLoginLogs()
    //{
    //    var logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
    //    if (!Directory.Exists(logsDirectory))
    //    {
    //        Directory.CreateDirectory(logsDirectory);
    //    }

    //    var logPath = Path.Combine(logsDirectory, "login_activity.log");
    //    if (!System.IO.File.Exists(logPath))
    //    {
    //        System.IO.File.WriteAllText(logPath, ""); // إنشاء ملف فارغ إذا لم يكن موجودًا
    //    }

    //    var logs = System.IO.File.ReadAllLines(logPath);
    //    return View(logs);
    //} 

    #endregion

    #region ViewLoginLogs2 Region

    //public IActionResult ViewLoginLogs()
    //{
    //    var logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
    //    if (!Directory.Exists(logsDirectory))
    //    {
    //        Directory.CreateDirectory(logsDirectory); // إنشاء المجلد إذا لم يكن موجودًا
    //    }

    //    var logPath = Path.Combine(logsDirectory, "login_activity.log");
    //    if (!System.IO.File.Exists(logPath))
    //    {
    //        System.IO.File.WriteAllText(logPath, ""); // إنشاء ملف فارغ إذا لم يكن موجودًا
    //    }

    //    // قراءة السجلات
    //    var logs = System.IO.File.ReadAllLines(logPath);
    //    return View(logs); // إرسال السجلات إلى العرض
    //}


    #endregion

    #region ViewLoginLogs3 Region

    //public async Task<IActionResult> ViewCustomerLogs()
    //{
    //    var logPath = Path.Combine(Directory.GetCurrentDirectory(), "logs", "customer_activity.log");

    //    if (!System.IO.File.Exists(logPath))
    //    {
    //        System.IO.File.WriteAllText(logPath, ""); // إنشاء ملف إذا لم يكن موجودًا
    //    }

    //    var logs = await System.IO.File.ReadAllLinesAsync(logPath);
    //    return View(logs);
    //}

    #endregion


    #endregion





    //ViewBag.customerCount = customersRepo.GetAll().Count();
    //ViewBag.ProductCount = productsRepo.GetAll().Count();
    //ViewBag.AllpriceCount = productsRepo.GetAll().Sum(e => e.Price);
    //ViewBag.Allproducts = productsRepo.GetAll();
    //        ViewBag.AllcategoryNames = categoriesRepo.GetAll();



    //[Authorize(Roles = "admin")]
    //[Authorize(Roles = "ADMIN")]
    //AdminDashboard


    #endregion
}













