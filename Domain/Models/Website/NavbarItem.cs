 

using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Website
{
    public class NavbarItem
    {
        [Key]
        public int NavBarItemId { get; set; }
        public string Title { get; set; } // عنوان آیتم
        public string Url { get; set; } // آدرس آیتم
        public int? Order { get; set; } // ترتیب نمایش
        public bool IsActive { get; set; } // وضعیت آیتم
        public int? ParentId { get; set; } // آیتم والد (null اگر اصلی باشد)
        public virtual NavbarItem? ParentNavbarItem { get; set; } // والد آیتم
        public virtual ICollection<NavbarItem>? Children { get; set; } // زیرمجموعه‌ها
    }



}
