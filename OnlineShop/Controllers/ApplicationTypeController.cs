using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly OnlineShopDbContext db;

        public ApplicationTypeController(OnlineShopDbContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            IEnumerable<ApplicationType> appTypeList = db.ApplicationType;
            return View(appTypeList);
        }

        [HttpGet]
        public IActionResult AddApplicationType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddApplicationType(ApplicationType applicationType)
        {
            if (ModelState.IsValid)
            {
                await db.ApplicationType.AddAsync(applicationType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(applicationType);
        }

        [HttpGet]
        public async Task<IActionResult> EditApplicationType(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var appType = await db.ApplicationType.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            return View(appType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditApplicationType(ApplicationType appType)
        {
            if (ModelState.IsValid)
            {
                db.ApplicationType.Update(appType);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(appType);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteApplicationType(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var appType = await db.ApplicationType.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            return View(appType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteApplicationTypeConfirmed(int? id)
        {
            var appType = await db.ApplicationType.FindAsync(id);

            if (appType == null)
            {
                return NotFound();
            }

            db.ApplicationType.Remove(appType);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
