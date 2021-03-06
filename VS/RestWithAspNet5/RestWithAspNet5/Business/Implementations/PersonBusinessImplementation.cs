using RestWithAspNet5.Data.Converter.Implementation;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Hypermedia.Utils;
using RestWithAspNet5.Model;
using RestWithAspNet5.Repository;
using System.Collections.Generic;

namespace RestWithAspNet5.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
             
        private readonly IPersonRepository  _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository )
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
        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parce(personEntity);

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

        public List<PersonVO> FinByName(string firstName, string lastName)
        {
            return _converter.Parce(_repository.FinByName(firstName,lastName));
        }

        public PersonVO Update(PersonVO person)
        {

            var personEntity = _converter.Parce(person);
            personEntity = _repository.Update(personEntity); // trabalha com entidade

            return _converter.Parce(personEntity);// retorna vo

        }

        public PageSearchVO<PersonVO> FindWithpagedSearch(string name, string sortDirection, int pageSize, int page)
        {
           
            var sort= (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc")?"asc":"desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @" select * from person p where 1=1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and  first_name like '%{name}%' ";
               query += $"  order by first_name asc limit {size} offset {offset}";

     
            string countQuery = @" select* from person p where 1 = 1";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and  first_name like '%{name}%' ";

            var persons = _repository.FindWithPagedSearch(query);

            int totalresult = _repository.GetCount(countQuery);

            return new PageSearchVO<PersonVO> {
            CurrentPage =page, 
            List = _converter.Parce(persons), 
            PageSize =size,
            SortDirections = sort, 
            TotalResults = totalresult

            };
        }
    }
}
