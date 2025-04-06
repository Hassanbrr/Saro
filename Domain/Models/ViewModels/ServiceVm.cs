using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Models.ViewModels
{
    public class ServiceVm
    {
        public Service Services { get; set; }
        public IEnumerable<SelectListItem>? CategoryList { get; set; }

    }
}
