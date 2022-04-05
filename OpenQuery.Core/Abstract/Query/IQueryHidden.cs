using OpenQuery.Core.Abstract.Clauses.Select;

namespace OpenQuery.Core.Abstract.Query
{
    public interface IQueryHidden : IQuery
    {
        ISelectedQuery Select(Func<ISelectClauseFactory, SelectExpression> expression);
    }
}