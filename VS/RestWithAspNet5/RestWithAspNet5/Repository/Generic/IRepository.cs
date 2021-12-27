using RestWithAspNet5.Model;
using RestWithAspNet5.Model.Base;
using System.Collections.Generic;

namespace RestWithAspNet5.Repository
{
    public interface IRepository<T> where T: BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exists(long id);


    }
}
