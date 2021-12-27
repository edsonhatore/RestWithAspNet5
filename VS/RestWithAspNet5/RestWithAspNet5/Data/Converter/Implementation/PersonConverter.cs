using RestWithAspNet5.Data.Converter.Contract;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using System.Collections.Generic;

namespace RestWithAspNet5.Data.Converter.Implementation
{
    public class PersonConverter : IParcer<PersonVO, Person>, IParcer<Person, PersonVO>
    {
        public Person Parcer(PersonVO origem)
        {
            throw new System.NotImplementedException();
        }



        public PersonVO Parcer(Person origem)
        {
            throw new System.NotImplementedException();
        }

        public List<PersonVO> parcer(List<Person> origem)
        {
            throw new System.NotImplementedException();
        }
        public List<Person> parcer(List<PersonVO> origem)
        {
            throw new System.NotImplementedException();
        }
    }

    }
