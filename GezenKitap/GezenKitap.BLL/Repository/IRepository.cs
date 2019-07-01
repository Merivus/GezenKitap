using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.BLL.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();     //Select * işlemi
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate); // select * from ... where ...
        T GetById(int id); //select * from ... where id=..
        T Get(Expression<Func<T, bool>> predicate); //select ... from ... where(...)
        void Add(T entity);     //insert into
        void Update(T entity);      //update
        void Delete(T entity);      //delete
        bool Delete(int id);        //delete from ... where(id=...)
        void AddRange(List<T> entities);    //birden fazla insert işlemi
    }
}
