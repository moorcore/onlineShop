using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly OnlineShopDbContext db;

        public CategoryController(OnlineShopDbContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            IEnumerable<Category> categoriesList = db.Categories;
            return View(categoriesList);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = await db.Categories.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Update(category);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = await db.Categories.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryConfirmed(int? id)
        {
            var category = await db.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
