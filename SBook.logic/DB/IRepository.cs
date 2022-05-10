using SBook.logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBook.logic.DB
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll
        {
            get;
        }
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int Id);

    }
}
