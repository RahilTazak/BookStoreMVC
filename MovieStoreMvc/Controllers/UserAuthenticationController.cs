using Microsoft.AspNetCore.Mvc;
using BookStoreMvc.Models.DTO;
using BookStoreMvc.Repositories.Abstract;

namespace BookStoreMvc.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        /* We will create a user with admin rights, after that we are going
          to comment this method because we need only
          one user in this application 
          If you need other users,you can implement this registration method with view
         */

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            /*
            var model = new RegistrationModel
            {
                Email = "admin@gmail.com",
                Username = "admin",
                Name = "Rahil",
                Password = "Admin@123",
                PasswordConfirm = "Admin@123",
                Role = "Admin"
            };*/
            // Change Role="User" if you want to register with User
            var result = await authService.RegisterAsync(model);
            if (result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData["msg"] = "Could not Register..";
                return RedirectToAction(nameof(Register));
            }
        }
        

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData["msg"] = "Could not logged in..";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
           await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}
