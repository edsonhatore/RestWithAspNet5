using RestWithAspNet5.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Model
{
    [Table("books")]
        public class Books : BaseEntity
        {
            [Column("author")]// igual ao banco      
            public string Author { get; set; }

            [Column("launch_date")]// igual ao banco      
            public DateTime Launch_Date { get; set; }

            [Column("price")]// igual ao banco      
            public double   Price { get; set; }

            [Column("title")]// igual ao banco      
            public string Title { get; set; }

        
    }
}
