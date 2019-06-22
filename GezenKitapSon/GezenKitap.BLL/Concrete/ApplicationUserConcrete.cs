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
    public class ApplicationUserConcrete
    {
        public IRepository<ApplicationUser> ApplicationUserRepository;
        public IUnitOfWork ApplicationUserUnitOfWork;
        private ApplicationDbContext _dbContext;

        public ApplicationUserConcrete()
        {
            _dbContext = new ApplicationDbContext();
            ApplicationUserUnitOfWork = new EFUnitOfWork(_dbContext);
            ApplicationUserRepository = ApplicationUserUnitOfWork.GetRepository<ApplicationUser>();
        }
    }
}
