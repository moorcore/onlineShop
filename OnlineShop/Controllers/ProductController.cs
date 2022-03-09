using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly OnlineShopDbContext db;

        public ProductController(OnlineShopDbContext context)
        {
            db = context;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> productsList = db.Products;

            foreach (var o in productsList)
            {
                o.Category = await db.Categories.FirstOrDefaultAsync(u => u.Id == o.CategoryId);
            }

            return View(productsList);
        }

        [HttpGet]
        public async Task <IActionResult> UpsertProduct(int? id)
        {
            IEnumerable<SelectListItem> CategoryDropdown = db.Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            ViewBag.CategoryDropdown = CategoryDropdown;

            Product product = new Product();
            if (id == null)
            {
                return View(product);
            }
            else
            {
                product = await db.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpsertProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(product);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = await db.Products.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(int? id)
        {
            var product = await db.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
