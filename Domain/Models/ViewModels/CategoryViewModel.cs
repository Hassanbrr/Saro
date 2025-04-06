 
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Models.ViewModels
{

    public class CategoryViewModel
    {
        public Category Category { get; set; }

        // لیستی از دسته‌بندی‌ها برای پر کردن Select Item
        public IEnumerable<SelectListItem>? ParentCategories { get; set; }
    }
}