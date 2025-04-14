using System.IO;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing; // برای تغییر اندازه

namespace Utility.Helpers
{
    public static class ImageHelper
    {
        public static async Task ResizeImageAsync(IFormFile uploadedFile, string savePath)
        {
            // بررسی فایل ورودی
            if (uploadedFile == null || uploadedFile.Length == 0)
            {
                throw new ArgumentException("فایل آپلود شده معتبر نیست.");
            }

            // باز کردن فایل و تغییر اندازه
            using (var image = await Image.LoadAsync(uploadedFile.OpenReadStream()))
            {
                // تغییر اندازه تصویر به 1800x1440
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(1440, 1800),
                    Mode = ResizeMode.Crop // برش تصویر برای تطابق کامل با اندازه
                }));

                // ذخیره تصویر با فرمت JPEG
                await image.SaveAsJpegAsync(savePath);
            }
        }
    }
}