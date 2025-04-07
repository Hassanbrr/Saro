using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Base;
using DataAccess.Context;
using DataAccess.Modules.Website.NavBarItem.Interfaces;
using Domain.Models.Website;

namespace DataAccess.Modules.Website.NavBarItem.Implements
{
    public class NavBarItemRepository:Repository<NavbarItem>,INavBarItemRepository
    {
        public NavBarItemRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
