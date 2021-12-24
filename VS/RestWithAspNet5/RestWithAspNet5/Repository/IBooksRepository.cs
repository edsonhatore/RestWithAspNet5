using RestWithAspNet5.Model;
using System.Collections.Generic;

namespace RestWithAspNet5.Repository
{
    public interface IBooksRepository
    {
        Books Create(Books book);
        Books FindById(long id);
        List<Books> FindAll();
        Books Update(Books book);
        void Delete(long id);
        bool Exists(long id);


    }
}
