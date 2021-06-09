using RestWithASPNETUdemy.Hypermedia;
using RestWithASPNETUdemy.Hypermedia.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestWithASPNETUdemy.Data.VO
{
    public class PersonVO : ISupportsHyperMedia
    {
        //jsonpropertyname = muda o nome que mostra no json 
        //[JsonPropertyName("codeID")]
        public long Id { get; set; }
        public string FirstName { get; set; }

        //[JsonIgnore] ignora o retorno do json
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        //HATEOAS 
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
