using Microsoft.AspNetCore.Mvc;

namespace Salon.Areas.Clint.ViewComponents
{
    public class NavSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
