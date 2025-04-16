using DataAccess.Base;
using Microsoft.AspNetCore.Mvc;

namespace Salon.Areas.Clint.Controllers
{
    [Area("Clint")]
    public class ServiceController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult GetServicesByCategory(int categoryId)
        {
            var services = _unitOfWork.Service.FindByCondition(s => s.CategoryId == categoryId).ToList();
            return View("Index",services);
        }
      
    }
}
