using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDMan.Models;
using SDMan.ViewModel;
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
        private readonly RoleManager<IdentityRole<int>> roleManager;
        //private readonly UserModel userModel;


        public AccountController(SignInManager<UserModel> _signInManager,
            UserManager<UserModel> _userManager,RoleManager<IdentityRole<int>> _roleManager)
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
            //Tylko do pierwszego uruchomienia.
            //IdentityRole<int> identity = new IdentityRole<int>
            //{
            //    Name = "Administrator"
            //};
            //await roleManager.CreateAsync(identity);
            //var admin = new UserModel { UserName = "Administrator" };

            //await userManager.CreateAsync(admin, "Administrator");
            //await userManager.AddToRoleAsync(admin, "Administrator");

            if (ModelState.IsValid)
            {
                
                var user = new UserModel { UserName = viewModel.UserName,Email=viewModel.UserName, FirstName=viewModel.FirstName,LastName=viewModel.LastName};
                
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
                IdentityRole<int> identity = new IdentityRole<int>
                {
                    Name = createRoleViewModel.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identity);
                if (result.Succeeded)
                {
                    return RedirectToAction("index","Home");
                }
            }
            
            return View();
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

    }
}