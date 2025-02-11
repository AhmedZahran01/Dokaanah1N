using Dokaanah.Models;
using Dokaanah.PresentationLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Dokaanah.Controllers
{
    public class Auth_AccountController : Controller
    {
		#region Constractor Region

		private readonly UserManager<Customer> userManager1;
		private readonly SignInManager<Customer> signInManager1;
		public Auth_AccountController(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
		{
			userManager1 = userManager;
			signInManager1 = signInManager;
		}

		#endregion
	

		#region Sign up (Register)

		// Auth_Account/SignUp
		public IActionResult SignUp()
        {
            return View();
        }
		 
        [HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModels models)
		{
            if (ModelState.IsValid)
            {
				var user = await userManager1.FindByNameAsync(models.UserName);
                if (user == null)
                { 
					user = new Customer()
					{
						UserName = models.UserName,
						Email = models.Email,
						Password = models.Password,
						confirmPassword = models.confirmPassword,
						isAgree = models.IsAgree,
					};
					 
					var result =	await userManager1.CreateAsync(user, models.Password);
					if(result.Succeeded)
					{
						return RedirectToAction(nameof(Signin) );
					}
					foreach (var error in result.Errors) 
					{
						ModelState.AddModelError(string.Empty , error.Description);
					}
				}

				ModelState.AddModelError(string.Empty, "user name is already exist");
            }
            return View();
		}
            
		#endregion


		#region Sign in (Login)

		// Auth_Account/SignUp
		public IActionResult Signin()
		{
			return View();
		}
		 
		#endregion
		 
	}
}
