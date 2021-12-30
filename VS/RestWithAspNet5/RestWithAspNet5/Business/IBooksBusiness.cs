using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using System.Collections.Generic;

namespace RestWithAspNet5.Business
{
    public interface IBooksBusiness
    {
        BooksVO Create(BooksVO book);
        BooksVO FindById(long id);
        List<BooksVO> FindAll();
        BooksVO Update(BooksVO person);
        void Delete(long id);

    }
}
