using System.Collections.Generic;

namespace RestWithAspNet5.Data.Converter.Contract
{
    public interface IParcer<O,D>
    {
        D Parce(O origem);
        List<D> parce(List<O> origem); 
    }
}
