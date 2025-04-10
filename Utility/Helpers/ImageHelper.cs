using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

 
namespace Utility.Helpers
{
 

    public static class ImageHelper
    {
        public static void ResizeAndSaveImage(Stream inputStream, string outputPath, int width, int height)
        {
            using (var image = Image.Load(inputStream)) // بارگذاری تصویر از استریم
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(width, height),
                    Mode = ResizeMode.Crop // تغییر اندازه به حالت Crop
                }));

                // ذخیره تصویر به مسیر مشخص
                image.Save(outputPath);
            }
        }
    }
}
