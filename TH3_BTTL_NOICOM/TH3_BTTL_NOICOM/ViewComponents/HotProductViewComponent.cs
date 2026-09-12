using Microsoft.AspNetCore.Mvc;
using TH3_BTTL_NOICOM.Models;

namespace TH3_BTTL_NOICOM.ViewComponents
{
    public class HotProductViewComponent : ViewComponent
    {
        protected Product product = new Product();

        public IViewComponentResult Invoke()
        {
            var products = product.GetProductList();
            return View(products);
        }
    }
}
