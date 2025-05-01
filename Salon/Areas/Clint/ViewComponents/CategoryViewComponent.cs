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
            // فقط دسته‌بندی‌هایی که والد هستند و فرزند ندارند
            var categories = _unitOfWork.Category
                .FindAll()
                .Where(c => c.ParentCategoryId == null)      
                .ToList();
            return View(categories);
        }
    }
}