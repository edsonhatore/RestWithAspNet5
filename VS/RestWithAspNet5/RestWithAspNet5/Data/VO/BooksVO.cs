using System;

namespace RestWithAspNet5.Data.VO
{


    public class BooksVO
    {

        public long Id { get; set; }
        public string Author { get; set; }

         
        public DateTime Launch_Date { get; set; }
     
        public double Price { get; set; }

          
        public string Title { get; set; }


    }
}
