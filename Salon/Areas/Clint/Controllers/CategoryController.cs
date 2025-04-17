using DataAccess.Base;
using Microsoft.AspNetCore.Mvc;

namespace Salon.Areas.Clint.Controllers
{
    [Area("Clint")]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [Route("category/{slug}")]
       
        public IActionResult GetServicesByCategory(string slug)
        {
            var formattedName = slug.Replace('-', ' '); // تبدیل slug به نام دسته‌بندی

            var category = _unitOfWork.Category
                .FindByCondition(c => c.CategoryName.ToLower() == formattedName.ToLower())
                .FirstOrDefault();

            if (category == null)
            {
                return NotFound(); // دسته‌بندی پیدا نشد، صفحه 404 نمایش بده
            }

            ViewBag.CategoryName = category.CategoryName;
            var services = _unitOfWork.Service
                .FindByCondition(s => s.CategoryId == category.CategoryId)
                .ToList();
            
            return View(services);
        }

    }
}
