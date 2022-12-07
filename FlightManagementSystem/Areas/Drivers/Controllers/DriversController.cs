using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CabManagementSystem.Data;
using CabManagementSystem.Models;

namespace CabManagementSystem.Areas.Drivers.Controllers
{
    [Area("Drivers")]
    public class DriversController : Controller
    {
        private readonly ApplicationDbContext db;

        public DriversController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Drivers/Drivers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.Drivers.Include(d => d.CabDriver);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Drivers/Drivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Drivers == null)
            {
                return NotFound();
            }

            var driver = await db.Drivers
                .Include(d => d.CabDriver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // GET: Drivers/Drivers/Create
        public IActionResult Create()
        {
            ViewData["DriverId"] = new SelectList(db.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Drivers/Drivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DriverViewModel model,string id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new Driver()
            {
                LicenseNumber = model.LicenseNumber,
                CarName = model.CarName,
                CarNumber = model.CarNumber,
                CarModel= model.CarModel,
                DriverId = id
            };
            await db.AddAsync(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Login", "Home", new { Area = "Accounts" });
        }

        // GET: Drivers/Drivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Drivers == null)
            {
                return NotFound();
            }

            var driver = await db.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(db.ApplicationUsers, "Id", "Id", driver.DriverId);
            return View(driver);
        }

        // POST: Drivers/Drivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarName,CarNumber,CarModel,LicenseNumber,DriverId")] Driver driver)
        {
            if (id != driver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(driver);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(db.ApplicationUsers, "Id", "Id", driver.DriverId);
            return View(driver);
        }

        // GET: Drivers/Drivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Drivers == null)
            {
                return NotFound();
            }

            var driver = await db.Drivers
                .Include(d => d.CabDriver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Drivers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Drivers'  is null.");
            }
            var driver = await db.Drivers.FindAsync(id);
            if (driver != null)
            {
                db.Drivers.Remove(driver);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverExists(int id)
        {
          return db.Drivers.Any(e => e.Id == id);
        }
    }
}
