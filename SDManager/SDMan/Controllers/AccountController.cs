using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SDMan.Context;
using SDMan.Models;
using SDMan.ViewModel;
using System;
using System.Collections.Generic;
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
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly SDManDbContext _context;
        //private readonly UserModel userModel;


        public AccountController(SignInManager<UserModel> _signInManager,
            UserManager<UserModel> _userManager,RoleManager<IdentityRole<int>> _roleManager, SDManDbContext context)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
            _context = context;



        }
       
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.ListGroup = new SelectList(_context.Groups.Select(x => x.Name).ToList(),model.Groupname);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
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
                
                //var user = new UserModel { UserName = viewModel.UserName,Email=viewModel.UserName, FirstName=viewModel.FirstName,LastName=viewModel.LastName};
                var user = new UserModel { UserName = viewModel.UserName  };
                //await userManager.AddToRoleAsync(user, "Employee");
                var result = await userManager.CreateAsync(user, viewModel.Password);
                
                if (result.Succeeded)
                {
                    
                        return RedirectToAction("Index", "Home");
                   
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
        public IActionResult GetRoles()
        {
            LoginViewModel model = new LoginViewModel();
            model.ListRole = roleManager.Roles.ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(int id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id.ToString());
            
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                Rolename = role.Name
            };

            var users = userManager.Users.ToList();

            // Retrieve all the Users
            foreach (var user in users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await userManager.IsInRoleAsync(user,role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id.ToString());

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.Rolename;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();
            var users = userManager.Users.ToList();
            foreach (var user in users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id.ToString(),
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }
        //public async Task<IActionResult> EditRole(int id)
        //{

        //    var user = await userManager.FindByIdAsync(id.ToString());
        //    user.ListRole = new SelectList(roleManager.Roles.ToList(),user.Role);
        //    return View(user);
        //}
        //[HttpPost]
        //public async  Task<IActionResult> EditSave(UserModel model, int id)
        //{
        //    try
        //    {
        //       // string role = model.Role.Name;
        //        model = await userManager.FindByIdAsync(id.ToString());
        //        //await userManager.UpdateAsync(model);
        //        var result = await userManager.UpdateAsync(model);
        //        if (result.Succeeded)
        //        {

        //            return RedirectToAction("Index", "Home");

        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //        //model = await userManager.FindByIdAsync(id.ToString());
        //        //model.ListRole = new SelectList(roleManager.Roles.ToList(), model.Role);


        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        Console.WriteLine(e.InnerException);
        //        throw;
        //    }
        //    return Redirect("Index");
        //}
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