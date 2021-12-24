using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Model
{
    
    [Table("person")]
    public class Person
    {
        [Column("id")]// igual ao banco        
        public long Id { get; set; }

        [Column("first_name")]// igual ao banco      
        public string FirstName { get; set; }

        [Column("last_name")]// igual ao banco      
        public string LastName { get; set; }

        [Column("address")]// igual ao banco      
        public string Address { get; set; }

        [Column("gender")]// igual ao banco      
        public string Gender { get; set; }

    }
}
