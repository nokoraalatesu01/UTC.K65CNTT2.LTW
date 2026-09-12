using Microsoft.AspNetCore.Mvc;
using SachMVCLab03.Models;

namespace SachMVCLab03.ViewComponents
{
    public class BookViewComponent : ViewComponent
    {
        protected Book book = new Book();
        public IViewComponentResult Invoke()
        {
            var books = book.GetBooksList();
            return View(books);
        }
    }
}
