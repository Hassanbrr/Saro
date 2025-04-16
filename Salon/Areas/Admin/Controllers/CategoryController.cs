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

namespace Salon.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = Sd.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var categoriesList = _unitOfWork.Category.FindAll(includeProperties: "ParentCategory").ToList();

            return View(categoriesList);
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {



            var categories = _unitOfWork.Category.FindAll().ToList();
            var viewModel = new CategoryViewModel
            {
                ParentCategories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }),
                Category = new Category()
            };

            if (id == 0 || id == null)
            {
                //create
                return View(viewModel);
            }
            else
            {
                //update
                viewModel.Category = _unitOfWork.Category.FindByCondition(u => u.CategoryId == id).FirstOrDefault();
                return View(viewModel);
            }

        }
        [HttpPost]

        public IActionResult Upsert(CategoryViewModel obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {


                if (file != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    // نام فایل نهایی
                    string fileName = Guid.NewGuid().ToString() + Path.GetFileNameWithoutExtension(file.FileName) + ".jpg";

                    // مسیر ذخیره نهایی
                    string productPath = Path.Combine(wwwRootPath, @"Images\Category");

                    // حذف تصویر قدیمی اگر وجود داشته باشد
                    if (!string.IsNullOrEmpty(obj.Category.ImageUrl))
                    {
                        var OldImagePath = Path.Combine(wwwRootPath, obj.Category.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Category.ImageUrl = @"\Images\Category\" + fileName; // مسیر ذخیره شده برای پایگاه داده
                }
                if (obj.Category.CategoryId == 0)
                {
                    obj.Category.CreatedAt = DateTime.Now; 
                    _unitOfWork.Category.Create(obj.Category);
                }
                else
                {
                    var existingService = _unitOfWork.Category.FindByCondition(u => u.CategoryId == obj.Category.CategoryId).AsNoTracking().FirstOrDefault(); // دریافت رکورد موجود
                    obj.Category.CreatedAt = existingService.CreatedAt;
                    obj.Category.UpdatedAt=DateTime.Now; 
                    _unitOfWork.Category.Update(obj.Category);
                }
                _unitOfWork.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            else
            {
                // در صورت خطا، مجدداً لیست دسته‌بندی‌های والد را ارسال کن
                var categories = _unitOfWork.Category.FindAll().ToList();
                CategoryViewModel ViewModel = new()
                {

                    ParentCategories = categories.Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    }),
                    Category = new Category()

                };
                return View(obj);
            }
        }

        public async Task<RedirectToActionResult> Delete(int id)
        {
            // قطع وابستگی فرزندان
            _unitOfWork.Category.DetachChildren(c => c.ParentCategoryId == id, "ParentCategoryId");

            // حذف والد
            var parent = await _unitOfWork.Category.FindByCondition(c => c.CategoryId == id).FirstOrDefaultAsync();
            if (parent != null)
            {
                _unitOfWork.Category.Delete(parent);
                _unitOfWork.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
