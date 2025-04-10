using DataAccess.Base;
using Microsoft.AspNetCore.Mvc;

namespace Salon.Areas.Clint.ViewComponents
{
    public class HeroViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

       
        public HeroViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IViewComponentResult Invoke()
        {

            var obj = _unitOfWork.HeroSection.FindAll().ToList();
            return View(obj);
        }
    }
}
