using System.Text;
using OpenQuery.Core.Abstract;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Tokens;

namespace OpenQuery.Core.Tokens
{
    internal class WhereEqual<TSource, T> : WhereTokenBase<TSource, T>
    {
        internal WhereEqual(ISqlDialect dialect, string name, T val) 
            : base(dialect, name, val)
        {
        }

        public override StringBuilder GetSign()
        {
            return new StringBuilder()
                .Append(Dialect.WhiteSpace)
                .Append(Dialect.IsEqual)
                .Append(Dialect.WhiteSpace);
        }
    }
}