using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Base;
using DataAccess.Context;
using DataAccess.Modules.Categories.Interfaces;

namespace DataAccess.Modules.Categories.Implements
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
