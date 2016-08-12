namespace OpenQuery.PCL.Abstract
{
    internal interface IHaveWhereClause : IQueryBase
    {
        IQuery AndWhere();
        IQuery OrWhere();
    }
}