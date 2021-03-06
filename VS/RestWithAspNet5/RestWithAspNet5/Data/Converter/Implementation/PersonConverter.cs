using RestWithAspNet5.Data.Converter.Contract;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5.Data.Converter.Implementation
{
    public class PersonConverter : IParcer<PersonVO, Person>, IParcer<Person, PersonVO>
    {
        public Person Parce(PersonVO origem)
        {
            if (origem ==null)
            {
                return null;
                }
            return new Person
            {
                Id = origem.Id, 
                FirstName = origem.FirstName,
                LastName = origem.LastName,
                Address = origem.Address,
                Gender = origem.Gender
            };

        }



        public PersonVO Parce(Person origem)
        {
            if (origem == null)
            {
                return null;
            }
            return new PersonVO
            {
                Id = origem.Id,
                FirstName = origem.FirstName,
                LastName = origem.LastName,
                Address = origem.Address,
                Gender = origem.Gender
            };
        }

        public List<PersonVO> Parce(List<Person> origem)
        {
            if (origem == null)
            {
                return null;
            }
            return origem.Select(item => Parce(item)).ToList();
        }
        public List<Person> Parce(List<PersonVO> origem)
        {
            if (origem == null)
            {
                return null;
            }
            return origem.Select(item => Parce(item)).ToList();
        }
    }

    }
