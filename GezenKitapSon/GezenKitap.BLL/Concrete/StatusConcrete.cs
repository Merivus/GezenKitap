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
    public class StatusConcrete
    {
        public IRepository<Status> StatusRepository;
        public IUnitOfWork StatusUnitOfWork;
        private ApplicationDbContext _dbContext;

        public StatusConcrete()
        {
            _dbContext = new ApplicationDbContext();
            StatusUnitOfWork = new EFUnitOfWork(_dbContext);
            StatusRepository = StatusUnitOfWork.GetRepository<Status>();
        }
    }
}
