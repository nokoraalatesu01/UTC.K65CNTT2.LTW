using Microsoft.AspNetCore.Mvc;
using NetCoreLA7.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NetCoreLA7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
         private static List<Category> _categories = new List<Category>()
        {
            new Category { Id = 1, Name = "Công nghệ thông tin", Status = 1, Description = "Sách và tài liệu IT" },
            new Category { Id = 2, Name = "Kinh tế - Quản trị", Status = 1, Description = "Sách kinh doanh" }
        };

      
        public IActionResult Index()
        {
            return View(_categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (_categories.Any(c => c.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("Name", "Tên danh mục đã tồn tại");
            }

            if (ModelState.IsValid)
            {
                category.Id = _categories.Count > 0 ? _categories.Max(c => c.Id) + 1 : 1;
                _categories.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (_categories.Any(c => c.Id != category.Id && c.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("Name", "Tên danh mục đã tồn tại");
            }

            if (ModelState.IsValid)
            {
                var existing = _categories.FirstOrDefault(c => c.Id == category.Id);
                if (existing != null)
                {
                    existing.Name = category.Name;
                    existing.Status = category.Status;
                    existing.Image = category.Image;
                    existing.Description = category.Description;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        public IActionResult Delete(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _categories.Remove(category);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}