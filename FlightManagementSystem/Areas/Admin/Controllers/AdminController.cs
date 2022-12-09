using CabManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CabManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            
            this.userManager = userManager;
            this.db = db;
        }
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await db.ApplicationUsers.ToListAsync());
        }



        // GET: AdminController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await db.ApplicationUsers.FindAsync(id);
            return View(new EditViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }

        // POST: AdminController/Edit/5

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model,string id)
        {

            if (!ModelState.IsValid)
                return View(model);
            var user = await db.ApplicationUsers.FindAsync(id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;



            db.ApplicationUsers.Update(user);
            db.SaveChanges();
            return RedirectToAction(nameof(Edit));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await db.ApplicationUsers.FindAsync(id);
            db.ApplicationUsers.Remove(user);
            await db.SaveChangesAsync(); 
            return RedirectToAction(nameof(Details));
        }

        
    }
}
