using RestWithAspNet5.Model;
using RestWithAspNet5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {

        private MySQLContext _context;

        public PersonRepositoryImplementation(MySQLContext context )
        {
            _context = context;
        }

        public Person Create(Person person)
        {

            try
            {
                _context.Add(person);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return person;

        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(v => v.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }


        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

       
        public Person FindById(long id)
        {

            return _context.Persons.SingleOrDefault(v => v.Id.Equals(id));
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return null;
            
            var result = _context.Persons.SingleOrDefault(v => v.Id.Equals(person.Id));
            if (result !=null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }


            return person;

        }

        public bool Exists(long id)
        {
            return _context.Persons.Any(v => v.Id.Equals(id));
        }
    }
}
