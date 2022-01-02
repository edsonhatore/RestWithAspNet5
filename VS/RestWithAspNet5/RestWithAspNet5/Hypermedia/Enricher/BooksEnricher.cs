using Microsoft.AspNetCore.Mvc;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Hypermedia.Constants;
using System.Text;
using System.Threading.Tasks;

namespace RestWithAspNet5.Hypermedia.Enricher
{

    public class BooksEnricher : ContentResponseEnricher<BooksVO>
    {
        private readonly object _lok = new object();

      
        protected override Task EnrichModel(BooksVO content, IUrlHelper urlHelper)
        {
            var path = "api/book/v1";
            string link = getLink(content.Id, urlHelper, path);

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });


            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PATCH,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPatch
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });


            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });


            return null;
        }

        private string getLink(long id, IUrlHelper urlHelper, string path)
        {lock (_lok)
            {
                var url = new { controller = path, id = id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            
            }
        }
    }
}
