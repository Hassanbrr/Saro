using DataAccess.Base;
using Microsoft.AspNetCore.Mvc;

namespace Salon.Areas.Clint.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;


        public CategoryViewComponent(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _unitOfWork.Category.FindAll().ToList();
            return View(categories);
        }
    }
}