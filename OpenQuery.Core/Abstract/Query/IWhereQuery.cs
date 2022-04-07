using OpenQuery.Core.Abstract.Clauses.Where;

namespace OpenQuery.Core.Abstract.Query
{
    public interface IWhereQuery : IQueryBase
    {
        IQuery Where(WhereFactoryExpression where);
    }
}