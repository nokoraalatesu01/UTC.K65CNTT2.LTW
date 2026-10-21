using Microsoft.AspNetCore.Mvc;
using TH_Lab01_QLSV.Models;

namespace TH_Lab01_QLSV.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        private List<MenuItem> MenuItems = new List<MenuItem>();

        public RenderViewComponent()
        {
            MenuItems = new List<MenuItem>()
            {
                new MenuItem() { Id = 1, Name = "Branches", Link = "/Branches/List" },
                new MenuItem() { Id = 2, Name = "Students", Link = "/Admin/Student/List" },
                new MenuItem() { Id = 3, Name = "Subjects", Link = "/Subjects/List" },
                new MenuItem() { Id = 4, Name = "Courses",  Link = "/Courses/List" }
            };
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View("RenderLeftMenu", MenuItems));
        }
    }
}
