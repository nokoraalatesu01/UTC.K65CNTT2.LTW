using Microsoft.AspNetCore.Mvc;
using TH_Lab01_QLSV.Data;
using TH_Lab01_QLSV.Models;

namespace TH_Lab01_QLSV.ViewComponents
{
    public class MajorViewComponent : ViewComponent
    {
        SchoolContext db;
        List<Major> majors;

        public MajorViewComponent(SchoolContext _context)
        {
            db = _context;
            majors = db.Majors.ToList();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View("RenderMajor", majors));
        }
    }
}