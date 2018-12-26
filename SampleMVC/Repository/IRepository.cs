using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleMVC.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        T Get(int id);
        bool Add(T entity);
        bool Delete(int id);
        bool Update(T entity);
    }
}