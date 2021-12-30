using System.Collections.Generic;

namespace RestWithAspNet5.Data.Converter.Contract
{
    public interface IParcer<O,D>
    {
        D Parce(O origem);
        List<D> Parce(List<O> origem); 
    }
}
