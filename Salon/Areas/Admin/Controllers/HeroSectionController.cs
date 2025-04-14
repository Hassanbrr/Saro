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
                    //finaly name
                    string fileName = Guid.NewGuid().ToString() + Path.GetFileNameWithoutExtension(file.FileName) + ".jpg";
                    //finaly masir
                    string productPath = Path.Combine(wwwRootPath, @"Images\Hero");

                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        //delete old image
                        var OldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }

                    }

                    //uplode new image
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }


                    obj.ImageUrl = @"\Images\Hero\" + fileName;

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
