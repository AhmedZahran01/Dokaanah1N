using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dokaanah.Models;
using Dokaanah.Repositories.RepoInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Dokaanah.PresentationLayer.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;
 
namespace Dokaanah.Controllers
{
    public class CustomersController : Controller
    {
        #region  Constractor Region

        private readonly ICustomersRepo _customersRepo;
        private readonly IOrdersRepo _ordersRepo1;
        private readonly UserManager<Customer> _userManager1;
        private readonly SignInManager<Customer> _signInManager1;
        private readonly ILogger<CustomersController> _logger;
 
        public CustomersController(ICustomersRepo customersRepo, IOrdersRepo ordersRepo,
                                   UserManager<Customer> userManager, SignInManager<Customer> signInManager,
                                   ILogger<CustomersController> logger, DokkanahDBContex dokkanahDBContex)
        {
            _customersRepo = customersRepo;
            _ordersRepo1 = ordersRepo;
            this._userManager1 = userManager;
            this._signInManager1 = signInManager;
            this._logger = logger;
        }


        #endregion

        #region Details Region

        // GET: Customers/Details/5
        [Authorize]
        public IActionResult DetailsForCustomer()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = _customersRepo.GetByUserByEmail(email);

            return View(user);
        }

        #region Admin Details Region

        // GET: Customers/Details/5
        [Authorize(Roles = "Admin")]
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customersRepo.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        #endregion


        #endregion

        #region  login and register and Sign Out

        #region Register Region
        #region MyRegion
        //#region Register Region

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(SignUpViewModels Registermodels)
        //{

        //    var userEmail = await _userManager1.FindByEmailAsync(Registermodels.Email);
        //    //var userName = await userManager1.FindByNameAsync(models.UserName);

        //    if (userEmail is null)
        //    {
        //        var userEmailCustomer = new Customer()
        //        {
        //            UserName = Registermodels.UserName,
        //            Email = Registermodels.Email,
        //            Password = Registermodels.Password,
        //            confirmPassword = Registermodels.confirmPassword,
        //            isAgree = Registermodels.IsAgree,
        //        };

        //        var result = await _userManager1.
        //                                  CreateAsync(userEmailCustomer, Registermodels.Password);

        //        if (result.Succeeded)
        //        {
        //            var LoginUserFromRegister = new signinviewmodel()
        //            {
        //                Email = Registermodels.Email,
        //                Password = Registermodels.Password,

        //            };
        //            await LoginAfterRegister(LoginUserFromRegister);
        //            return RedirectToAction(nameof(HomeController.Index), "Home");

        //        }

        //        if (!result.Succeeded)
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }

        //            return RedirectToAction(nameof(CustomersController.RegisterErrors), "Customers", Registermodels);
        //        }
        //    }

        //    ModelState.AddModelError(string.Empty, " email is already exist");

        //    //return View(nameof(Auth_AccountController.SignUp), "Auth_Account");
        //    return RedirectToAction(nameof(RegisterErrors));

        //}

        //#endregion 
        #endregion
       
        
        #region new 

        #region Register Region

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SignUpViewModels registerModel)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }

            // Check if the email already exists
            var existingUser = await _userManager1.FindByEmailAsync(registerModel.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Email is already registered.");
                return View(registerModel);
            }

            // Create a new customer
            var newCustomer = new Customer
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                isAgree = registerModel.IsAgree,
                Password = registerModel.Password,
                confirmPassword = registerModel.confirmPassword
            };

            // Create user with the password
            var creationResult = await _userManager1.CreateAsync(newCustomer, registerModel.Password);
            if (creationResult.Succeeded)
            {
                // Auto-login after successful registration
                var loginModel = new signinviewmodel
                {
                    Email = registerModel.Email,
                    Password = registerModel.Password
                };
                await LoginAfterRegister(loginModel);

                // Redirect to home page
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            // Handle errors during user creation
            foreach (var error in creationResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerModel);
        }

        #endregion


        #endregion

        #region Register Errors Region

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterErrors(SignUpViewModels Registermodels)
        {

            var userEmail = await _userManager1.FindByEmailAsync(Registermodels.Email);
            //var userName = await userManager1.FindByNameAsync(models.UserName);

            if (userEmail is null)
            {
                var userEmailCustomer = new Customer()
                {
                    UserName = Registermodels.UserName,
                    Email = Registermodels.Email,
                    Password = Registermodels.Password,
                    confirmPassword = Registermodels.confirmPassword,
                    isAgree = Registermodels.IsAgree,
                };

                var result = await _userManager1.
                                          CreateAsync(userEmailCustomer, Registermodels.Password);

                if (result.Succeeded)
                {
                    var LoginUserFromRegister = new signinviewmodel()
                    {
                        Email = Registermodels.Email,
                        Password = Registermodels.Password,

                    };
                    await LoginAfterRegister(LoginUserFromRegister);
                    return View(Registermodels);

                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return RedirectToAction(nameof(HomeController.Index), "Home");

            }

            ModelState.AddModelError(string.Empty, " email is already exist");

            //return View(nameof(Auth_AccountController.SignUp), "Auth_Account");
            return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        #endregion

        #endregion

        #region Login Region


        #region Login Region

        #region Comment Login

        //public async Task<IActionResult> Login(signinviewmodel models, string returnUrl = null)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        var checkUser = await _userManager1.FindByEmailAsync(models.Email);
        //        if (checkUser is not null)
        //        {
        //            var flag = await _userManager1.CheckPasswordAsync(checkUser, models.Password);
        //            if (flag)
        //            {
        //                var result = await _signInManager1.
        //                    PasswordSignInAsync(checkUser, models.Password, models.RemmeberMe, lockoutOnFailure: false);

        //                if (result.Succeeded)
        //                {
        //                    if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
        //                    {
        //                        return RedirectToAction(nameof(AdminDashboardController.Index), "AdminDashboard");

        //                    }
        //                    _logger.LogInformation($"Login: User {models.Email} logged in at {DateTime.UtcNow}.");

        //                    return RedirectToAction(nameof(HomeController.Index), "Home");
        //                }

        //            }

        //        }
        //        ModelState.AddModelError(string.Empty, " Login is Not Valid");
        //    }

        //    return RedirectToAction(nameof(HomeController.Index), "Home");

        //}

        //public async Task<IActionResult> Login(signinviewmodel models, string returnUrl = null)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var checkUser = await _userManager1.FindByEmailAsync(models.Email);
        //        if (checkUser is not null)
        //        {
        //            var flag = await _userManager1.CheckPasswordAsync(checkUser, models.Password);
        //            if (flag)
        //            {
        //                var result = await _signInManager1.
        //                    PasswordSignInAsync(checkUser, models.Password, models.RemmeberMe, lockoutOnFailure: false);

        //                if (result.Succeeded)
        //                {
        //                    if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
        //                    {
        //                        return RedirectToAction(nameof(AdminDashboardController.Index), "AdminDashboard");
        //                    }

        //                    if (User.Identity.IsAuthenticated)
        //                    {
        //                        var email = User.FindFirstValue(ClaimTypes.Email);
        //                        var user = _customersRepo.GetByUserByEmail(email);
        //                        user.IsOnline = true;
        //                        // تسجيل بيانات تسجيل الدخول
        //                        _logger.LogInformation($"Login: User {models.Email} logged in at {DateTime.UtcNow}.", new { user = models.Email });
        //                        dokkanahDBContex.SaveChanges();
        //                        return RedirectToAction(nameof(HomeController.Index), "Home");

        //                    }
        //                    return RedirectToAction(nameof(HomeController.Index), "Home");
        //                }
        //            }
        //        }
        //        ModelState.AddModelError(string.Empty, "Login is Not Valid");
        //    }

        //    return RedirectToAction(nameof(HomeController.Index), "Home");
        //} 
        #endregion

        #region MyRegion

        //public async Task<IActionResult> Login(signinviewmodel models)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var checkUser = await _userManager1.FindByEmailAsync(models.Email);
        //        if (checkUser is not null)
        //        {
        //            var flag = await _userManager1.CheckPasswordAsync(checkUser, models.Password);
        //            if (flag)
        //            {
        //                var result = await _signInManager1.PasswordSignInAsync(checkUser, models.Password, models.RemmeberMe, lockoutOnFailure: false);

        //                if (result.Succeeded)
        //                {
        //                    //// سجل معلومات الدخول
        //                    //var logger = NLog.LogManager.GetCurrentClassLogger();
        //                    //logger.Info($"Login: User {checkUser.Email} logged in at {DateTime.UtcNow}.");

        //                    if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
        //                    {

        //                        return RedirectToAction(nameof(AdminDashboardController.Index), "AdminDashboard");
        //                    }
        //                    //var email = User.FindFirstValue(ClaimTypes.Email);
        //                    //var user = _customersRepo.GetByUserByEmail(email);
        //                    //user.IsOnline = true;


        //                    var userId = _userManager1.GetUserId(User);
        //                    var customer = await _userManager1.FindByIdAsync(userId);
        //                       // تحديث حالة المستخدم إلى Online
        //                        customer.IsOnline = true;
        //                        await _userManager1.UpdateAsync(customer);

        //                    // تسجيل الدخول في الـ Log
        //                    var logger = NLog.LogManager.GetCurrentClassLogger();
        //                    logger.Info($"Customer Login: {customer.UserName} logged in at {DateTime.UtcNow}."); 

        //                    //// تحديث حالة الـ Customer إلى "Online"
        //                    // await _userManager1.UpdateAsync(user);

        //                    // استدعاء SignalR لتحديث الحالة في الـ Admin Dashboard
        //                    var hubContext = HttpContext.RequestServices.GetRequiredService<IHubContext<UserHub>>();
        //                    await hubContext.Clients.All.SendAsync("ReceiveUserStatus", userId, "Online");

        //                    return RedirectToAction(nameof(HomeController.Index), "Home");
        //                }
        //            }
        //        }

        //        ModelState.AddModelError(string.Empty, "Login is Not Valid");
        //    }

        //    return RedirectToAction(nameof(LoginErrorView), "Customers");
        //} 
        #endregion

        public async Task<IActionResult> Login(signinviewmodel models)
        {
            if (ModelState.IsValid)
            {
                var checkUser = await _userManager1.FindByEmailAsync(models.Email);
                if (checkUser != null)
                {
                    var flag = await _userManager1.CheckPasswordAsync(checkUser, models.Password);
                    if (flag)
                    {
                        var result = await _signInManager1.PasswordSignInAsync(checkUser, models.Password, models.RemmeberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            var userId = _userManager1.GetUserId(User);
                            var customer = await _userManager1.FindByIdAsync(userId);
                            if (customer != null)
                            {
                                customer.IsOnline = true;
                                await _userManager1.UpdateAsync(customer);

                                if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                                {

                                    return RedirectToAction(nameof(AdminDashboardController.Index), "AdminDashboard");
                                }
                                // Notify admin dashboard via SignalR
                                var hubContext = HttpContext.RequestServices.GetRequiredService<IHubContext<UserHub>>();
                                await hubContext.Clients.All.SendAsync("ReceiveUserStatus", userId, "Online");

                                // Log login activity
                                var authLogger = NLog.LogManager.GetLogger("AuthLogger");
                                authLogger.Info($"Customer Login:      ({customer.UserName}) " +
                                    $"  with id ({customer.Id}) logged in at ({DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm")}).");
                            }

                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Login is Not Valid");
            }

            return RedirectToAction(nameof(LoginErrorView), "Customers");
        }


        public async Task<IActionResult> LoginErrorView(signinviewmodel models)
        {
            return View();
        }
        #endregion

        #region Login After Register Region

        public async Task<IActionResult> LoginAfterRegister(signinviewmodel models, string returnUrl = null)
        {


            var checkUser = await _userManager1.FindByEmailAsync(models.Email);
            if (checkUser is not null)
            {
                var flag = await _userManager1.CheckPasswordAsync(checkUser, models.Password);
                if (flag)
                {
                    var result = await _signInManager1.
                        PasswordSignInAsync(checkUser, models.Password, models.RemmeberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }

                }

            }


            return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        #endregion

        #endregion

        #region Sign Out  Region
        #region MyRegion

        //[Authorize]  
        //public async Task<IActionResult> SignOut(SignUpViewModels models)
        //{
        //    #region Comment Save Changes Region

        //    //var email = User.FindFirstValue(ClaimTypes.Email);
        //    //var user = _customersRepo.GetByUserByEmail(email);
        //    //user.IsOnline = false;
        //    //dokkanahDBContex.SaveChanges(); 
        //    #endregion

        //    var userId = _userManager1.GetUserId(User);
        //    Customer customer = new Customer();
        //    if (!string.IsNullOrEmpty(userId))
        //    {
        //         customer = await _userManager1.FindByIdAsync(userId);
        //        if (customer != null)
        //        {
        //            // تحديث حالة المستخدم إلى Offline
        //            customer.IsOnline = false;
        //            await _userManager1.UpdateAsync(customer);

        //            // استدعاء SignalR لتحديث الحالة في الـ Admin Dashboard
        //            var hubContext = HttpContext.RequestServices.GetRequiredService<IHubContext<UserHub>>();
        //            await hubContext.Clients.All.SendAsync("ReceiveUserStatus", customer.Id, "Offline");
        //        }
        //    } 

        //    await _signInManager1.SignOutAsync();
        //    // تسجيل الخروج في الـ Log
        //    var logger = NLog.LogManager.GetCurrentClassLogger();
        //    logger.Info($"Customer Logout: {customer.UserName} logged out at {DateTime.UtcNow}.");

        //    return RedirectToAction("Index", "Home");

        //    #region Comment Log File  Region

        //    //return RedirectToAction(nameof(HomeController.Index), "Home");

        //    //var userName = User.Identity.Name ?? "Anonymous";
        //    //await _signInManager1.SignOutAsync();
        //    //_logger.LogInformation($"Logout: User {userName} logged out at {DateTime.UtcNow}.");
        //    //return RedirectToAction(nameof(HomeController.Index), "Home");

        //    #endregion

        //} 
        #endregion


        [Authorize]
        public async Task<IActionResult> SignOut(SignUpViewModels models)
        {
            var userId = _userManager1.GetUserId(User);
            Customer customer = null;
            if (!string.IsNullOrEmpty(userId))
            {
                customer = await _userManager1.FindByIdAsync(userId);
                if (customer != null)
                {
                    customer.IsOnline = false;
                    await _userManager1.UpdateAsync(customer);

                    // Notify admin dashboard via SignalR
                    var hubContext = HttpContext.RequestServices.GetRequiredService<IHubContext<UserHub>>();
                    await hubContext.Clients.All.SendAsync("ReceiveUserStatus", customer.Id, "Offline");
                }
            }

            await _signInManager1.SignOutAsync();

            // Log logout activity
            var authLogger = NLog.LogManager.GetLogger("AuthLogger");
            authLogger.Info($"Customer Logout: ({customer?.UserName ?? "Unknown"})   with id ({customer.Id}) logged out at ({DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm")}).");

            return RedirectToAction("Index", "Home");
        }

        #endregion


        #endregion

        #region Create  Region

        #region Create Customer By Admin  Region

        // GET: Customers/Create
        [Authorize(Roles = "Admin")]
        public IActionResult CreateByAdmin()
        {
            return View();
        }

        #endregion

        #region  Create POST Region

        #region Create Post Customer By Admin Region


        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateByAdmin([Bind("UserName,Email,Password,confirmPassword,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customersRepo.Insert(customer);
                return RedirectToAction(nameof(AllCustomersRegion), "Customers");
            }
            ViewBag.OrderidList = _ordersRepo1.GetAll();
            return View(customer);
        }


        #endregion


        #region Create HttpPost Region

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Email,Password,confirmPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                //_customersRepo.Insert(customer);
                _userManager1.CreateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OrderidList = _ordersRepo1.GetAll();
            return View(customer);
        }

        #endregion


        #endregion

        #endregion

        #region   Edit region

        #region Edit HttpGet Region

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult EditByAdmin(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customersRepo.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewBag.OrderidList = _ordersRepo1.GetAll();
            return View(customer);
        }

        #endregion

        #region Edit POST By Admin Region

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditByAdmin([Bind("Id,UserName,Password,Address")] Customer customer)
        {

            #region  comment Edit Region
            //if (ModelState.IsValid)
            //{ 
            //    var entity = _customersRepo.GetById(customer.Id);
            //    if (entity == null)
            //    {
            //        return NotFound("The customer no longer exists.");
            //    }

            //    //_customersRepo.Update(customer);
            //    entity.UserName = customer.UserName;
            //    entity.Address = customer.Address;
            //    entity.Password = customer.Password;
            //    entity.confirmPassword = customer.Password;
            //    entity.PasswordHash = customer.Password;
            //    await _userManager1.UpdateAsync(entity);
            //    return RedirectToAction(nameof(AllCustomersRegion), "Customers");
            //}
            //ViewData["Orderid"] = _ordersRepo1.GetAll(); 
            #endregion

            if (ModelState.IsValid)
            {
                var entity = _customersRepo.GetById(customer.Id);
                if (entity == null)
                {
                    return NotFound("The customer no longer exists.");
                }

                // Update fields other than the password
                entity.UserName = customer.UserName;
                entity.Password = customer.Password;
                entity.confirmPassword = customer.Password;
                entity.Address = customer.Address;

                if (!string.IsNullOrWhiteSpace(customer.Password))
                {
                    // Update the password securely

                    #region  Comment Update Error Region
                    //var removePasswordResult = await _userManager1.RemovePasswordAsync(entity);
                    //if (!removePasswordResult.Succeeded)
                    //{
                    //    ModelState.AddModelError("",  removePasswordResult.Errors);
                    //    return View(customer); // Return the view with validation errors
                    //} 
                    #endregion

                    var removePasswordResult = await _userManager1.RemovePasswordAsync(entity);
                    if (!removePasswordResult.Succeeded)
                    {
                        foreach (var error in removePasswordResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description); // Add each error description to ModelState
                        }
                        return View(customer); // Return the view with validation errors
                    }

                    var addPasswordResult = await _userManager1.AddPasswordAsync(entity, customer.Password);
                    if (!addPasswordResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Failed to add the new password.");
                        return View(customer); // Return the view with validation errors
                    }
                }

                // Save the updated user details
                var updateResult = await _userManager1.UpdateAsync(entity);
                if (!updateResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to update user details.");
                    return View(customer); // Return the view with validation errors
                }

                return RedirectToAction(nameof(AllCustomersRegion), "Customers");
            }

            return View(customer);
        }

        #endregion

        #region   Edit User Account region

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditUserAccount([Bind("UserName,Password,Address")] Customer Editcustomer)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var customer = _customersRepo.GetByUserByEmail(email);
            if (ModelState.IsValid)
            {
                // Update fields other than the password
                customer.UserName = Editcustomer.UserName;
                customer.Address = Editcustomer.Address;

                if (!string.IsNullOrWhiteSpace(Editcustomer.Password))
                {
                    // Update the password securely
                    var removePasswordResult = await _userManager1.RemovePasswordAsync(customer);
                    if (!removePasswordResult.Succeeded)
                    {
                        foreach (var error in removePasswordResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description); // Add each error description to ModelState
                        }
                        var Data = new Customer()
                        {
                            Email = email,
                            UserName = Editcustomer.UserName,
                            Id = Editcustomer.Id,
                            Address = Editcustomer.Address,
                        };
                        ViewBag.CurrentUserData = Data;
                        return View(Data); // Return the view with validation errors
                    }

                    var addPasswordResult = await _userManager1.AddPasswordAsync(customer, Editcustomer.Password);
                    if (!addPasswordResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Failed to add the new password.");
                        var Data = new Customer()
                        {
                            Email = email,
                            UserName = Editcustomer.UserName,
                            Id = Editcustomer.Id,
                            Address = Editcustomer.Address,
                        };
                        ViewBag.CurrentUserData = Data;
                        return View(Data); // Return the view with validation errors
                    }
                }

                // Save the updated user details
                var updateResult = await _userManager1.UpdateAsync(customer);
                if (!updateResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to update user details.");
                    var Data = new Customer()
                    {
                        Email = email,
                        UserName = Editcustomer.UserName,
                        Id = Editcustomer.Id,
                        Address = Editcustomer.Address,
                    };
                    ViewBag.CurrentUserData = Data;
                    return View(Data); // Return the view with validation errors
                }
            }

            ViewData["Orderid"] = _ordersRepo1.GetAll();
            return RedirectToAction(nameof(HomeController.UserAccount), "Home");



        }

        #endregion


        #endregion

        #region Delete Region

        #region Delete Region

        // GET: Customers/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customersRepo.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        #endregion

        #region  Delete Confirmed Region
        // POST: Customers/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = _customersRepo.GetById(id);
            if (customer != null)
            {
                _customersRepo.Delete(customer);
            }

            return RedirectToAction(nameof(AllCustomersRegion));
        }

        #endregion

        #endregion

        #region Customer Exists Region
        [Authorize(Roles = "Admin")]
        private bool CustomerExists(string id)
        {
            return _customersRepo.GetAll().Any(e => e.Id == id);
        }
        #endregion

        #region  All Customers Region

        [Authorize(Roles = "Admin")]
        public IActionResult AllCustomersRegion()
        {
            var allCustomers = _customersRepo.GetAll().Where(c => c.Id != "010a4e59-0459-4046-a280-09c66a35a7ba").ToList();
            ViewBag.allCustomers = allCustomers;
            return View();
        }

        #endregion
         
    }
}
