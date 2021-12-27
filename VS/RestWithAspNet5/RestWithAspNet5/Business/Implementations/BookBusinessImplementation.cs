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

        public BookBusinessImplementation(IRepository<Books> repository)
        {
            _repository = repository;
        }

        public Books Create(Books book)
        {


            return _repository.Create(book);

        }

        public void Delete(long id)
        {
            _repository.Delete(id);


        }

        public List<Books> FindAll()
        {
            return _repository.FindAll();
        }

        public Books FindById(long id)
        {

            return _repository.FindById(id);
        }

        public Books Update(Books person)
        {
            return _repository.Update(person);

        }

    }
}
