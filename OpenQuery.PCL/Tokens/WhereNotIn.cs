using System.Text;
using OpenQuery.PCL.Abstract;

namespace OpenQuery.PCL.Tokens
{
    internal class WhereNotIn<T> : WhereTokenBase<T>
    {
        internal WhereNotIn(ISqlImplementation implementation, string name, T val)
            : base(implementation, name, val)
        {
        }

        public override StringBuilder GetSign()
        {
            return new StringBuilder()
                .Append(Implementation.WhiteSpace)
                .Append(Implementation.NotIn)
                .Append(Implementation.WhiteSpace);
        }
    }
}