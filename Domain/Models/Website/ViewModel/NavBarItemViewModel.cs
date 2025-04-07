using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Models.Website.ViewModel
{
    public class NavbarItemViewModel
    {
        public NavbarItem NavbarItem { get; set; }
        public List<NavbarItem> ParentItems { get; set; } // لیست آیتم‌های والد برای انتخاب
        public IEnumerable<NavbarItem> NavbarList { get; set; } // لیست کل آیتم‌ها
    }

}
