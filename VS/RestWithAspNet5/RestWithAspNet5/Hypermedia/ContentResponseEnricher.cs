using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestWithAspNet5.Hypermedia.Abstract;
using RestWithAspNet5.Hypermedia.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspNet5.Hypermedia
{
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportsHyperMedia
    {

        public ContentResponseEnricher()
        {


        }

        public bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(List<T>) || contentType == typeof(PageSearchVO<T>); 

        }

        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);


        bool IResponseEnricher.CanEnrich(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult)
            {
                return CanEnrich(okObjectResult.Value.GetType());
            }
            return false;
        }
        public async  Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);
            if (response.Result is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }
                else if (okObjectResult.Value is List<T> collection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
                    Parallel.ForEach(bag, (element) =>
                    {
                        EnrichModel(element, urlHelper);

                    }
                    
                    );
                }
                else if (okObjectResult.Value is PageSearchVO<T> Pagedsearch)
                {
                    
                    Parallel.ForEach(Pagedsearch.List, (element) =>
                    {
                        EnrichModel(element, urlHelper);

                    }

                    );
                }

            }
         
            await Task.FromResult<object>(null);



        }
    }
}
