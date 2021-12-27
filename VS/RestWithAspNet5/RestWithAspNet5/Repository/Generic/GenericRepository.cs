using Microsoft.EntityFrameworkCore;
using RestWithAspNet5.Model.Base;
using RestWithAspNet5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        private MySQLContext _context;
        private DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }
      

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(v => v.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                  

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(v => v.Id.Equals(id));
        }

        public T Update(T item)
        {
            var result = dataset.SingleOrDefault(v => v.Id.Equals(item.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();

                    return result;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return null; 

            }
        }
            public bool Exists(long id)
            {
                return dataset.Any(v => v.Id.Equals(id));
            }

        
    }
}
