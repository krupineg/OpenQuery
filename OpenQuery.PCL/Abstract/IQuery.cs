using System.Collections.Generic;

namespace OpenQuery.PCL.Abstract
{
    public interface IQuery : IQueryBase
    {
        
    }

    public interface IQueryHidden : IQuery
    {
        ISelectedQuery Select(IList<string> fields);
        ISelectedQuery Select(params string[] fields);
    }
}