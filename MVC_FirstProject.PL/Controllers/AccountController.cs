using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_FirstProject.DAL.Models;
using MVC_FirstProject.PL.ViewModels.User;
using System.Threading.Tasks;

namespace MVC_FirstProject.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) 
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}



        [HttpGet]
        public IActionResult SignUp() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if(ModelState.IsValid)
            {
                if(user is null)
                {
					user = new ApplicationUser()
					{
						FName = model.FirstName,
						LName = model.LastName,
						UserName = model.Username,
						Email = model.Email,
						IsAgree = model.IsAgree,
					};

					var result = await _userManager.CreateAsync(user, model.Password);
					if(result.Succeeded)
						return RedirectToAction(nameof(SignIn));

					foreach(var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
				}
				ModelState.AddModelError(string.Empty, "This user name is already in use in another account");
			}
			return View(model);

		}
	}
}
