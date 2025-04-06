 
using System.Threading.Tasks;
using DataAccess.Modules.Categories.Interfaces;
using DataAccess.Modules.Services.Interfaces;

namespace DataAccess.Base
{
    public interface IUnitOfWork
    {
 ICategoryRepository Category { get; }
 IServiceRepository Service { get; }
 void SaveChanges();
    }
}
