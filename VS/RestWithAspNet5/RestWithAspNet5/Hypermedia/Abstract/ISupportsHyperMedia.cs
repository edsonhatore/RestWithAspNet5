using System.Collections.Generic;

namespace RestWithAspNet5.Hypermedia.Abstract
{
    public interface ISupportsHyperMedia
    {
        List<HyperMediaLink>Links { get; set; }
    }
}
