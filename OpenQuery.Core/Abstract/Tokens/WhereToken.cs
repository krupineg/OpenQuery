using System.Text;
using OpenQuery.Core.Abstract.Clauses.Where;
using OpenQuery.Core.Abstract.Dialect;

namespace OpenQuery.Core.Abstract.Tokens
{
    internal abstract class WhereToken<T> : IToken
    {
        private readonly WhereExpression _whereExpression;
        private readonly T _val;

        internal WhereToken(WhereExpression whereExpression, T val)
        {
            _whereExpression = whereExpression;
            _val = val;
        }

        protected abstract StringBuilder GetSign(ISqlDialect dialect);

        protected virtual string ValueToString(T value, ISqlDialect dialect)
        {
            return value.ToString();
        }
        
        public string Build(ISqlDialect dialect)
        {
            return new StringBuilder(_whereExpression(dialect)).Append(GetSign(dialect))
                .Append(ValueToString(_val, dialect))
                .ToString();
        }
    }
}