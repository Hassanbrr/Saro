using DataAccess.Base;
using Domain.Models.ViewModels;
using Domain.Models.Website;
using Domain.Models.Website.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace Salon.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = Sd.Role_Admin)]
    public class NavBarItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NavBarItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var navbarItems = _unitOfWork.NavBarItem
                .FindAll(includeProperties:"Children")
                .Where(n => n.ParentId == null)
                .OrderBy(n => n.Order)
                .ToList();

            return View(navbarItems);


        }
        public IActionResult Create()
        {
            var viewModel = new NavbarItemViewModel
            {
                NavbarItem = new NavbarItem(),
                ParentItems = _unitOfWork.NavBarItem.FindAll().Where(n => n.IsActive).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(NavbarItemViewModel viewModel)
        {
            ModelState.Remove("ParentItems");
            ModelState.Remove("NavbarList");

            if (ModelState.IsValid)
            {
                _unitOfWork.NavBarItem.Create(viewModel.NavbarItem);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            viewModel.ParentItems = _unitOfWork.NavBarItem.FindAll().Where(n => n.IsActive).ToList();
            return View(viewModel);
        }
        [HttpPost]
        public JsonResult UpdateOrder(List<int> orderedIds)
        {
            try
            {
                for (int i = 0; i < orderedIds.Count; i++)
                {
                    // استفاده از FindByCondition برای دریافت آیتم
                    var navbarItem = _unitOfWork.NavBarItem.FindByCondition(
                        n => n.NavBarItemId == orderedIds[i],
                        includeProperties: null // در صورت نیاز به بارگذاری داده‌های مرتبط، مشخص کنید
                    ).FirstOrDefault();

                    if (navbarItem != null)
                    {
                        navbarItem.Order = i + 1; // تنظیم ترتیب جدید
                        _unitOfWork.NavBarItem.Update(navbarItem); // به‌روزرسانی آیتم در Repository
                    }
                }

                // ذخیره تغییرات
                _unitOfWork.SaveChanges();
                return Json(new { success = true, message = "ترتیب با موفقیت ذخیره شد!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
