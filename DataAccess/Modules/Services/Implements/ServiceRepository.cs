using DataAccess.Base;
using DataAccess.Context;
using DataAccess.Modules.Services.Interfaces;



namespace DataAccess.Modules.Services.Implements
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
