using GezenKitap.BLL.Repository;
using GezenKitap.BLL.UnitOfWork;
using GezenKitap.DAL;
using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.BLL.Concrete
{
    public class ReviewConcrete
    {
        public IRepository<Review> ReviewRepository;
        public IUnitOfWork ReviewUnitOfWork;
        private ApplicationDbContext _dbContext;

        public ReviewConcrete()
        {
            _dbContext = new ApplicationDbContext();
            ReviewUnitOfWork = new EFUnitOfWork(_dbContext);
            ReviewRepository = ReviewUnitOfWork.GetRepository<Review>();
        }
    }
}
