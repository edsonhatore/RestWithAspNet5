using RestWithAspNet5.Data.Converter.Implementation;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using RestWithAspNet5.Repository;
using System.Collections.Generic;

namespace RestWithAspNet5.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
             
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> repository )
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {

            var personEntity = _converter.Parce(person);
            personEntity= _repository.Create(personEntity); // trabalha com entidade
            
            return _converter.Parce(personEntity);// retorna vo
        }

        public void Delete(long id)
        {
           _repository.Delete(id);


        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parce(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {

            return _converter.Parce(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {

            var personEntity = _converter.Parce(person);
            personEntity = _repository.Update(personEntity); // trabalha com entidade

            return _converter.Parce(personEntity);// retorna vo

        }

    }
}
