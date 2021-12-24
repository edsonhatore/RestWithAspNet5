using RestWithAspNet5.Model;
using System.Collections.Generic;

namespace RestWithAspNet5.Business
{
    public interface IBooksBusiness
    {
        Books Create(Books book);
        Books FindById(long id);
        List<Books> FindAll();
        Books Update(Books person);
        void Delete(long id);

    }
}
