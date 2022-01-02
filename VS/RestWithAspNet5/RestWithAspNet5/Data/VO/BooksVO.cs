using RestWithAspNet5.Hypermedia;
using RestWithAspNet5.Hypermedia.Abstract;
using System;
using System.Collections.Generic;

namespace RestWithAspNet5.Data.VO
{


    public class BooksVO: ISupportsHyperMedia
    {
        public long Id { get; set; }
        public string Author { get; set; }

         
        public DateTime Launch_Date { get; set; }
     
        public double Price { get; set; }

          
        public string Title { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
