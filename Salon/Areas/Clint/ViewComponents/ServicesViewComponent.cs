using DataAccess.Base;
using Microsoft.AspNetCore.Mvc;

namespace Salon.Areas.Clint.ViewComponents
{
    public class ServicesViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        
        public ServicesViewComponent(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public IViewComponentResult Invoke()
        {
           var serviceList = _unitOfWork.Service.FindAll().ToList();
            return View(serviceList);
        }
    }
}
