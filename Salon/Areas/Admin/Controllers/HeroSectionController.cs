using DataAccess.Base;
using Domain.Models.Website;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Salon.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroSectionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HeroSectionController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }

        public IActionResult Index()
        {
            var obj = _unitOfWork.HeroSection.FindAll().ToList();

            return View(obj);
        }

        public IActionResult Upsert(int? id, IFormFile? file)
        {
            HeroSection heroSection = new();

            if (id == 0 || id == null)
            {
                //create
                return View(heroSection);
            }
            else
            {
                //update
                var obj = _unitOfWork.HeroSection.FindByCondition(u => u.Id == id).FirstOrDefault();
                return View(obj);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(HeroSection obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // مسیر اصلی - دقت به حروف بزرگ/کوچک
                    string originalPath = Path.Combine(wwwRootPath, @"images\Hero", fileName);

                    using (var fileStream = new FileStream(originalPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    // تقسیم عکس
                    using (var image = Image.Load(file.OpenReadStream()))
                    {
                        int width = image.Width;
                        int height = image.Height;

                        // نیمه چپ
                        var leftHalf = image.Clone(ctx => ctx.Crop(new Rectangle(0, 0, width / 2, height)));
                        string leftPath = Path.Combine(wwwRootPath, @"images\Hero\left_" + fileName);
                        await leftHalf.SaveAsync(leftPath);

                        // نیمه راست
                        var rightHalf = image.Clone(ctx => ctx.Crop(new Rectangle(width / 2, 0, width / 2, height)));
                        string rightPath = Path.Combine(wwwRootPath, @"images\Hero\right_" + fileName);
                        await rightHalf.SaveAsync(rightPath);

                        // مقداردهی به obj اصلی (نه ایجاد شیء جدید)
                        obj.OriginalImagePath = @"\images\Hero\" + fileName;
                        obj.LeftPartPath = @"\images\Hero\left_" + fileName;
                        obj.RightPartPath = @"\images\Hero\right_" + fileName;
                    }

                    if (obj.Id == 0)
                    {
                        _unitOfWork.HeroSection.Create(obj);
                    }
                    else
                    {
                        _unitOfWork.HeroSection.Update(obj);
                    }

                    _unitOfWork.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else if (obj.Id == 0)
                {
                    ModelState.AddModelError("file", "برای رکورد جدید باید عکس آپلود شود.");
                }
            }

            return View(obj);
        }
    }
}
