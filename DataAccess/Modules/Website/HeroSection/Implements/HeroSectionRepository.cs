using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Base;
using DataAccess.Context;
using DataAccess.Modules.Website.HeroSection.Interfaces;

namespace DataAccess.Modules.Website.HeroSection.Implements
{
    public class HeroSectionRepository : Repository<Domain.Models.Website.HeroSection>, IHeroSectionRepository
    {
        public HeroSectionRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
