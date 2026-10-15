using Microsoft.AspNetCore.Mvc;

namespace NetCoreLA7.Areas.Admin.ViewComponents
{
    public class NavTopViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
