using RestWithAspNet5.Model;
using RestWithAspNet5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5.Repository.Implementations
{
    public class BookRepositoryImplementation : IBooksRepository
    {

        private MySQLContext _context;

        public BookRepositoryImplementation(MySQLContext context )
        {
            _context = context;
        }

        public Books Create(Books book)
        {

            try
            {
                _context.Add(book);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return book;

        }

        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(v => v.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }


        }

        public List<Books> FindAll()
        {
            return _context.Books.ToList();
        }

       
        public Books FindById(long id)
        {

            return _context.Books.SingleOrDefault(v => v.Id.Equals(id));
        }

        public Books Update(Books book)
        {
            if (!Exists(book.Id)) return null;
            
            var result = _context.Books.SingleOrDefault(v => v.Id.Equals(book.Id));
            if (result !=null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }


            return book;

        }

        public bool Exists(long id)
        {
            return _context.Books.Any(v => v.Id.Equals(id));
        }
    }
}
