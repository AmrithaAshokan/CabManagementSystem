
using CabManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace CabManagementSystem.Areas.Accounts.Controllers
{


    [Area("Accounts")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(
            ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid details");
                return View(model);
            }
            var res = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (res.Succeeded)
            {

                return Redirect("/accounts/home/booking");
            }
            ModelState.AddModelError("", "Invalid Email or Password");
            return View(model);

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = Guid.NewGuid().ToString().ToLower().Replace("-", ""),
            };
            var role = Convert.ToString(model.Roles);
            var res = await userManager.CreateAsync(user, model.Password);
            await userManager.AddToRoleAsync(user, role);
            if (res.Succeeded)
            {

                if (await userManager.IsInRoleAsync(user, "Driver"))
                {
                    return RedirectToAction("Create", "Drivers", new { Area = "Drivers",id=user.Id });
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
               

            foreach (var error in res.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
           

            

        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Booking()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Booking(BookingViewModel model)
        {

            if (model.From == model.To)
            {
                ModelState.AddModelError(nameof(model.To), "Invalid location.Drop-off and pick-Up cannot be same location");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            db.BookARide.Add(new Bookings()
            {
                To = model.To,
                From = model.From,
                BookingDate = (DateTime)model.BookingDate,
                NumberOfPassengers = model.NumberOfPassengers,
                CarModel= model.CarModel
            });
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

            public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var signeduser = await userManager.GetUserAsync(User);
            var user = await userManager.FindByEmailAsync(signeduser.Email);
            return View(new EditViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }



        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);
            var signeduser = await userManager.GetUserAsync(User);
            var user = await userManager.FindByEmailAsync(signeduser.Email);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;



            await userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Edit));
        }
        public async Task<IActionResult> Delete()
        {
            var signeduser = await userManager.GetUserAsync(User);
            var user = await userManager.FindByEmailAsync(signeduser.Email);
            await signInManager.SignOutAsync();
            await userManager.DeleteAsync(user);
            return Redirect("accounts/home/edit");
        }

        public async Task<IActionResult> GenerateData()
        {
            await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
            await roleManager.CreateAsync(new IdentityRole() { Name = "Driver" });

                var appUser = new ApplicationUser()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@mail.com",
                    UserName = "admin@777"
                };
                var res = await userManager.CreateAsync(appUser, "Pass@123");
                await userManager.AddToRoleAsync(appUser, "Admin");
            
            return Ok("Data generated");
        }
    }
   }
       



