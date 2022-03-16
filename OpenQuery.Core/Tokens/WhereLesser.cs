using System.Text;
using OpenQuery.Core.Abstract;

namespace OpenQuery.Core.Tokens
{
    internal class WhereLesser<T> : WhereTokenBase<T>
    {
        internal WhereLesser(ISqlImplementation implementation, string name, T val)
            : base(implementation, name, val)
        {
        }
        public override StringBuilder GetSign()
        {
            return new StringBuilder()
                .Append(Implementation.WhiteSpace)
                .Append(Implementation.Lesser)
                .Append(Implementation.WhiteSpace);
        }
    }
}