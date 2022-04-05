using OpenQuery.Core.Abstract.Clauses.From;

namespace OpenQuery.Core.Abstract.Query
{
    internal interface ISelectedQueryHidden : ISelectedQuery
    {
        IAvailableWhereQuery From(Func<IFromClauseFactory, FromExpression> func);
    }
}