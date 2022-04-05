using System.Diagnostics.Contracts;
using System.Text;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Reflection;

namespace OpenQuery.Core.Abstract.Tokens
{
    internal abstract class WhereTokenBase<TSource, T> : TokenBase
    {
        private readonly string _name;
        private readonly T _val;

        internal WhereTokenBase(ISqlDialect dialect, string name, T val) : base(dialect)
        {
            Contract.Assert(
                QueryFieldsCache.GetProperties(typeof(TSource)).Contains(name), 
                $"Field {name} is not available for {typeof(T).Name}");
            _val = val;
            _name = name;
        }

        public abstract StringBuilder GetSign();

        public override string Build()
        {
            return new StringBuilder(_name).Append(GetSign())
                .Append(_val)
                .ToString();
        }
    }
}