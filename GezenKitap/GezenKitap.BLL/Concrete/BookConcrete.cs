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
    public class BookConcrete
    {
        public IRepository<Book> BookRepository;
        public IUnitOfWork BookUnitOfWork;
        private ApplicationDbContext _dbContext;

        public BookConcrete()
        {
            _dbContext = new ApplicationDbContext();
            BookUnitOfWork = new EFUnitOfWork(_dbContext);
            BookRepository = BookUnitOfWork.GetRepository<Book>();
        }
    }
}
