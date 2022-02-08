using RestWithAspNet5.Hypermedia;
using RestWithAspNet5.Hypermedia.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestWithAspNet5.Data.VO
{


    public class PersonVO:ISupportsHyperMedia
    {
       // [JsonPropertyName("code")] // para conhecimento -> para mudar o nome na V 5 usa anotation
        [JsonPropertyName("Id")] // para conhecimento -> para mudar o nome na V 5 usa anotation
        public long Id { get; set; }       
        public string FirstName { get; set; }

     //   [JsonPropertyName("Last_Name")] // para conhecimento -> para mudar o nome na V 5 usa anotation
        public string LastName { get; set; }
       //  [JsonIgnore]
        public string Address { get; set; }
          
        public string Gender { get; set; }
        public bool Enabled { get; set; }
        public List<HyperMediaLink> Links { get  ; set; }=new List<HyperMediaLink>();
    }
}
