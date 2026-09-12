using Microsoft.AspNetCore.Mvc;
using SachMVCLab03.Models;

namespace SachMVCLab03.Controllers
{
    public class BookController : Controller
    {
        /// <summary>
        /// Authors : Nguyễn Đức Trường
        /// Lớp IT2
        /// MSSV 241230872
        /// </summary>
        protected Book book = new Book();
        public IActionResult Index()
        {
            ViewBag.authors = book.Authors;
            ViewBag.genres = book.Genres;
            var books = book.GetBooksList();
            return View(books);
        }
        public IActionResult Create()
        {
            ViewBag.authors = book.Authors;
            ViewBag.genres = book.Genres;
            Book model = new Book();
            return View(model);
        }

        public IActionResult Edit(int id) 
        {
            ViewBag.authors = book.Authors;
            ViewBag.genres = book.Genres;
            Book model = book.GetBookByID(id);
            return View(model);
        }

        public PartialViewResult PopularBook()
        {
            var books = book.GetBooksList();
            return PartialView(books);
        }
    }
}
