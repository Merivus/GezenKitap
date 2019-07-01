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
    public class OrderConcrete
    {
        public IRepository<Order> OrderRepository;
        public IUnitOfWork OrderUnitOfWork;
        private ApplicationDbContext _dbContext;

        public OrderConcrete()
        {
            _dbContext = new ApplicationDbContext();
            OrderUnitOfWork = new EFUnitOfWork(_dbContext);
            OrderRepository = OrderUnitOfWork.GetRepository<Order>();
        }
    }
}
