using DataAccess.Base;
using Microsoft.AspNetCore.Mvc;

namespace Salon.Areas.Clint.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    private readonly IUnitOfWork _unitOfWork;


    public FooterViewComponent(IUnitOfWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }
    public IViewComponentResult Invoke()
    {
    
        return View( );
    }
}