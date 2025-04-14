using System.Globalization;
using DataAccess.Base;
using DataAccess.Context;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Utility;
using Utility.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace Salon.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = Sd.Role_Admin)]

    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var servicesList = _unitOfWork.Service.FindAll(includeProperties: "Category").ToList();
            return View(servicesList);
        }


        public IActionResult Upsert(int? id, IFormFile? file)
        {
            ServiceVm serviceList = new()
            {

                CategoryList = _unitOfWork.Category.FindAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId.ToString()
                }),
                Services = new Service()
            };
            if (id == 0 || id == null)
            {
                //create
                return View(serviceList);
            }
            else
            {
                //update
                serviceList.Services = _unitOfWork.Service.FindByCondition(u => u.ServiceId == id).FirstOrDefault();
                return View(serviceList);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Upsert(ServiceVm obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    // نام فایل نهایی
                    string fileName = Guid.NewGuid().ToString() + Path.GetFileNameWithoutExtension(file.FileName) + ".jpg";

                    // مسیر ذخیره نهایی
                    string productPath = Path.Combine(wwwRootPath, @"Images\Service");

                    // حذف تصویر قدیمی اگر وجود داشته باشد
                    if (!string.IsNullOrEmpty(obj.Services.ImageUrl))
                    {
                        var OldImagePath = Path.Combine(wwwRootPath, obj.Services.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }
                    }

                    // تغییر اندازه تصویر و ذخیره آن
                    string savePath = Path.Combine(productPath, fileName); // مسیر ذخیره تصویر جدید
                    await ImageHelper.ResizeImageAsync(file, savePath); // استفاده از متد تغییر اندازه

                    obj.Services.ImageUrl = @"\Images\Service\" + fileName; // مسیر ذخیره شده برای پایگاه داده
                }

                if (!string.IsNullOrEmpty(obj.Services.Price.ToString()))
                {
                    string rawPrice = obj.Services.Price.ToString();
                    rawPrice = rawPrice.Replace(",", "").Replace("٬", ""); // حذف کاماهای فارسی
                    obj.Services.Price = decimal.Parse(rawPrice, CultureInfo.InvariantCulture); // تبدیل به عدد
                }

                // ذخیره یا بروزرسانی رکورد در پایگاه داده
                if (obj.Services.ServiceId == 0)
                {
                    obj.Services.CreatedAt = DateTime.Now;
                    _unitOfWork.Service.Create(obj.Services);
                }
                else
                {
                    var existingService = _unitOfWork.Service.FindByCondition(u => u.ServiceId == obj.Services.ServiceId)
                                                             .AsNoTracking()
                                                             .FirstOrDefault(); // دریافت رکورد موجود
                    obj.Services.CreatedAt = existingService.CreatedAt;
                    obj.Services.UpdatedAt = DateTime.Now;
                    _unitOfWork.Service.Update(obj.Services);
                }

                _unitOfWork.SaveChanges();
                TempData["success"] = "Service Created Successfully";

                return RedirectToAction("Index");
            }
            else
            {
                ServiceVm serviceList = new()
                {
                    CategoryList = _unitOfWork.Category.FindAll().Select(u => new SelectListItem
                    {
                        Text = u.CategoryName,
                        Value = u.CategoryId.ToString()
                    }),
                    Services = new Service()
                };

                return View(serviceList);
            }
        }

        public IActionResult Delete(int id)
        {
            var service = _unitOfWork.Service.FindByCondition(u => id == u.ServiceId).FirstOrDefault();
            if (service == null)
            {
                return NotFound();
            }

            _unitOfWork.Service.Delete(service);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
