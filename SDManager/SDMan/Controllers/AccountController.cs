using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SDMan.Models;
using SDMan.ViewModel;
//using SDMan.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Context
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserModel> signInManager;
        private readonly UserManager<UserModel> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        //private readonly UserModel userModel;
        public AccountController(SignInManager<UserModel> _signInManager,
            UserManager<UserModel> _userManager,RoleManager<IdentityRole> _roleManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            IdentityRole identity = new IdentityRole
            {
                Name = "Administrator"
            };
            await roleManager.CreateAsync(identity);
            var admin = new UserModel { UserName = "Administrator" };

            var adminresult = await userManager.CreateAsync(admin, "Administrator");
            await userManager.AddToRoleAsync(admin, "Administrator");

            if (ModelState.IsValid)
            {
                
                //var user = new UserModel { UserName = viewModel.UserName,Email=viewModel.UserName, FirstName=viewModel.FirstName,LastName=viewModel.LastName};
                var user = new UserModel { UserName = viewModel.UserName  };
                
                var result = await userManager.CreateAsync(user, viewModel.Password);
                
                if (result.Succeeded)
                {
                    var login = await signInManager.PasswordSignInAsync(viewModel.Email,
                                        viewModel.Password, true, false);
                    if (login.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Nie można się zalogować!");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(viewModel.UserName,
                                        viewModel.Password, true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nie można się zalogować!");
                }
            }
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identity = new IdentityRole
                {
                    Name = createRoleViewModel.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identity);
            }
            
            return View();
        }

        //public IActionResult ChangePwd()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangePwd(ResetPasswordViewModel model)
        //{
        //    // Validates the received password data based on the view model
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {

        //            var t = User.Identity.Name;
        //            var r = await userManager.FindByNameAsync(User.Identity.Name);

        //            //model.CurrentPassword = model.CurrentPassword;
        //            // model.NewPassword = model.NewPassword;
        //            var result = await userManager.ChangePasswordAsync(r, model.CurrentPassword, model.NewPassword);
        //            result = await userManager.UpdateAsync(r);
        //            if (result.Succeeded)
        //            {

        //                return RedirectToAction("Index", "Home");

        //            }
        //            else
        //            {
        //                return Redirect("Error");
        //            }


        //        }
        //        return Redirect("Error");
        //    }
        //    catch
        //    {

        //        return View("Error");
        //    }
        //}
        //public async Task<IActionResult> EditUserData()
        //{
        //    //var r = await userManager.FindByNameAsync(User.Identity.Name);
        //    //var claims = await userManager.GetClaimsAsync(r);
        //    //model.FirstName = claims.Where(c => c.Type == "FirstName").Select(c => c.Value).ToString();
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditUserData(EditUserDataViewModel model)
        //{
        //    // Validates the received password data based on the view model
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {

        //            //var user = User.Identity;
        //            //user = new EditUserDataViewModel {FirstName=model.FirstName,LastName=model.LastName,Email=model.Email };
        //            var r = await userManager.FindByNameAsync(User.Identity.Name);
        //            var claims = await userManager.GetClaimsAsync(r);
        //            var t = User;
        //            await userManager.RemoveClaimsAsync(r, claims);
        //            await userManager.AddClaimAsync(r, new Claim("FirstName", model.FirstName));
        //            await userManager.AddClaimAsync(r, new Claim("LastName", model.LastName));
        //            await userManager.GetClaimsAsync(r);
        //            await userManager.ChangeEmailAsync(r, model.Email, "");



        //            //var result = userManager.UpdateAsync(r);




        //            //var user = new UserModel();
        //            //user.Id = r.Id;
        //            //user.FirstName = model.FirstName;
        //            //user.LastName = model.LastName;
        //            // user.Email = model.Email;

        //            //model.FirstName = 
        //            //var result = await userManager.AddClaimAsync(r,new Claim("FirstName", model.FirstName));
        //            //result = await userManager.AddClaimAsync(r, new Claim("LastName", model.LastName));
        //            //result = await userManager.AddClaimAsync(r,new Claim("Email",model.Email));
        //            var result = await userManager.UpdateAsync(r);



        //            if (result.Succeeded)
        //            {

        //                return RedirectToAction("EditUserData", "Account");

        //            }
        //            else
        //            {
        //                return Redirect("Error");
        //            }


        //        }
        //        return Redirect("Error");
        //    }
        //    catch
        //    {

        //        return View(model);
        //    }
        //}
        //public UserModel Get(string id)
        //{
        //    id = User.Identity.Name;
        //    var r = userManager.FindByNameAsync(id);
        //    return r;
        //}
    }
}