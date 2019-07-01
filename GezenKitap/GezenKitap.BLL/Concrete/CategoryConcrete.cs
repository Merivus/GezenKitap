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
    public class CategoryConcrete
    {
        public IRepository<Category> CategoryRepository;
        public IUnitOfWork CategoryUnitOfWork;
        private ApplicationDbContext _dbContext;

        public CategoryConcrete()
        {
            _dbContext = new ApplicationDbContext();
            CategoryUnitOfWork = new EFUnitOfWork(_dbContext);
            CategoryRepository = CategoryUnitOfWork.GetRepository<Category>();
        }
    }
}
