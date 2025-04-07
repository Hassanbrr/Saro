 
using System.Threading.Tasks;
using DataAccess.Modules.Categories.Interfaces;
using DataAccess.Modules.Services.Interfaces;
using DataAccess.Modules.Website.NavBarItem.Interfaces;

namespace DataAccess.Base
{
    public interface IUnitOfWork
    {
 ICategoryRepository Category { get; }
 IServiceRepository Service { get; }
 INavBarItemRepository NavBarItem { get; }

void SaveChanges();
    }
}
