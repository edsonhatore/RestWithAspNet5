using RestWithAspNet5.Data.Converter.Contract;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5.Data.Converter.Implementation
{
    public class BooksConverter : IParcer<BooksVO, Books>, IParcer<Books, BooksVO>
    {
        public Books Parce(BooksVO origem)
        {
            if (origem ==null)
            {
                return null;
                }
            return new Books
            {
                 Id = origem.Id, 
                 Author = origem.Author,
                 Launch_Date = origem.Launch_Date,
                 Title = origem.Title,
                    Price = origem.Price
            };

        }



        public BooksVO Parce(Books origem)
        {
            if (origem == null)
            {
                return null;
            }
            return new BooksVO
            {
                Id = origem.Id,
                Author = origem.Author,
                Launch_Date = origem.Launch_Date,
                Title = origem.Title,
                Price = origem.Price
            };
        }

        public List<BooksVO> Parce(List<Books> origem)
        {
            if (origem == null)
            {
                return null;
            }
            return origem.Select(item => Parce(item)).ToList();
        }
        public List<Books> Parce(List<BooksVO> origem)
        {
            if (origem == null)
            {
                return null;
            }
            return origem.Select(item => Parce(item)).ToList();
        }
    }

    }
