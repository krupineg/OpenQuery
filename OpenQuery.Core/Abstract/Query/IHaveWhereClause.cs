namespace OpenQuery.Core.Abstract.Query
{
    internal interface IHaveWhereClause : IQueryBase
    {
        IQuery AndWhere();
        IQuery OrWhere();
    }
}