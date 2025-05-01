using DataAccess.Base;
using Microsoft.AspNetCore.Mvc;

namespace Salon.Areas.Clint.ViewComponents
{
    public class SearchGroupViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;


        public SearchGroupViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke()
        {

            var obj = _unitOfWork.Category.FindAll().ToList();
            return View(obj);
        }
    }
}
