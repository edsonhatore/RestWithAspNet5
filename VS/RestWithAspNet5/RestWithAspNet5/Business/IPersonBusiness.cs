using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Hypermedia.Utils;
using RestWithAspNet5.Model;
using System.Collections.Generic;

namespace RestWithAspNet5.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);

        List<PersonVO> FinByName(string firstName, string lastName);
        List<PersonVO> FindAll();

        PageSearchVO<PersonVO> FindWithpagedSearch(string name, string sortDirection, int pageSize, int page);
        PersonVO Update(PersonVO person);
        PersonVO Disable(long id);
        void Delete(long id);

    }
}
