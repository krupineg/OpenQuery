using System.Text;
using OpenQuery.Core.Abstract;
using OpenQuery.Core.Abstract.Clauses.Where;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Tokens;

namespace OpenQuery.Core.Tokens
{
    internal class WhereIn<T> : WhereToken<T[]>
    {
        internal WhereIn(WhereExpression whereExpression, T[] val)
            : base(whereExpression, val)
        {
        }
        
        protected override string ValueToString(T[] value, ISqlDialect dialect)
        {
            return dialect.CreateIn(value.Select(x => x.ToString()).ToArray()).ToString();
        }
        
        protected override StringBuilder GetSign(ISqlDialect dialect)
        {
            return new StringBuilder()
                .Append(dialect.WhiteSpace)
                .Append(dialect.In)
                .Append(dialect.WhiteSpace);
        }
    }
}