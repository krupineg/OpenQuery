using System.Text;
using OpenQuery.PCL.Abstract;

namespace OpenQuery.PCL.Tokens
{
    internal class WhereIn<T> : WhereTokenBase<T>
    {
        internal WhereIn(ISqlImplementation implementation, string name, T val)
            : base(implementation, name, val)
        {
        }

        public override StringBuilder GetSign()
        {
            return new StringBuilder()
                .Append(Implementation.WhiteSpace)
                .Append(Implementation.In)
                .Append(Implementation.WhiteSpace);
        }
    }
}