namespace OpenQuery.Core.Abstract
{
    public interface IQueryHidden : IQuery
    {
        ISelectedQuery Select(Func<SelectClauseFactory, SelectExpression> expression);
    }
}