using RestWithAspNet5.Data.Converter.Implementation;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using RestWithAspNet5.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Business.Implementations
{
    public class BookBusinessImplementation: IBooksBusiness
    {

        private readonly IRepository<Books> _repository;
        private readonly BooksConverter _converter;

        public BookBusinessImplementation(IRepository<Books> repository)
        {
            _repository = repository;
            _converter = new BooksConverter();
        }

        public BooksVO Create(BooksVO book)
        {
            var bookEntity = _converter.Parce(book);
            bookEntity = _repository.Create(bookEntity); // trabalha com entidade

            return _converter.Parce(bookEntity);// retorna vo


        }

        public void Delete(long id)
        {
            _repository.Delete(id);


        }

        public List<BooksVO> FindAll()
        {
            return _converter.Parce(_repository.FindAll());
        }

        public BooksVO FindById(long id)
        {

            return _converter.Parce(_repository.FindById(id));
        }

        public BooksVO Update(BooksVO book)
        {
            
            var bookEntity = _converter.Parce(book);
            bookEntity = _repository.Update(bookEntity); // trabalha com entidade

            return _converter.Parce(bookEntity);// retorna vo
        }

    }
}
