using DataAccess.Modules.Categories.Implements;
using DataAccess.Modules.Services.Interfaces;
using DataAccess.Modules.Services.Implements;
using DataAccess.Modules.Categories.Interfaces;
using DataAccess.Context;
using DataAccess.Modules.Website.HeroSection.Implements;
using DataAccess.Modules.Website.NavBarItem.Implements;
using DataAccess.Modules.Website.NavBarItem.Interfaces;
using DataAccess.Modules.Website.HeroSection.Interfaces;


namespace DataAccess.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
         
        public ICategoryRepository Category { get; private set; }
        public IServiceRepository Service { get; private set; }
        public INavBarItemRepository NavBarItem { get; private set; }
        public IHeroSectionRepository HeroSection { get; private set; }

        public UnitOfWork(ApplicationDbContext db) {

            _db = db;
            Service = new ServiceRepository(_db);
            Category = new CategoryRepository(_db); 
            NavBarItem = new NavBarItemRepository(_db);
            HeroSection = new HeroSectionRepository(_db);

        }



        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
