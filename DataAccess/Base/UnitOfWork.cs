using DataAccess.Modules.Categories.Implements;
using DataAccess.Modules.Services.Interfaces;
using DataAccess.Modules.Services.Implements;
using DataAccess.Modules.Categories.Interfaces;
using DataAccess.Context;



namespace DataAccess.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;


        public ICategoryRepository Category { get; private set; }
        public IServiceRepository Service { get; private set; }
        public UnitOfWork(ApplicationDbContext db) {

            _db = db;
            Service = new ServiceRepository(_db);
            Category = new CategoryRepository(_db);


        }



        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
