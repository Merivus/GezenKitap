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
    public class AuthorConcrete
    {
        public IRepository<Author> AuthorRepository;
        public IUnitOfWork AuthorUnitOfWork;
        private ApplicationDbContext _dbContext;

        public AuthorConcrete()
        {
            _dbContext = new ApplicationDbContext();
            AuthorUnitOfWork = new EFUnitOfWork(_dbContext);
            AuthorRepository = AuthorUnitOfWork.GetRepository<Author>();
        }
    }
}
