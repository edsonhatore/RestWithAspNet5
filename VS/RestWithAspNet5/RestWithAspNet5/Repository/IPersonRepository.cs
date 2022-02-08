using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);

        List<Person> FinByName(string firstName , string secondName);

    }
}
