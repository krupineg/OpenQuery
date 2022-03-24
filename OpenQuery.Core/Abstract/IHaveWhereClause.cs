namespace OpenQuery.Core.Abstract
{
    internal interface IHaveWhereClause : IQueryBase
    {
        IQuery AndWhere();
        IQuery OrWhere();
        IReadyToBuildQuery As(string alias);
    }
}