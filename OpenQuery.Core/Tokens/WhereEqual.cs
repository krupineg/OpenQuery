using System.Text;
using OpenQuery.Core.Abstract;
using OpenQuery.Core.Abstract.Clauses.Where;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Tokens;

namespace OpenQuery.Core.Tokens
{
    internal class WhereEqual<T> : WhereToken<T>
    {
        internal WhereEqual(WhereExpression whereExpression, T val) 
            : base(whereExpression, val)
        {
        }

        protected override StringBuilder GetSign(ISqlDialect dialect)
        {
            return new StringBuilder()
                .Append(dialect.WhiteSpace)
                .Append(dialect.IsEqual)
                .Append(dialect.WhiteSpace);
        }
    }
}