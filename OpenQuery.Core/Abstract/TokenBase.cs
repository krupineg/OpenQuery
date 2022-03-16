using System.Text;

namespace OpenQuery.Core.Abstract
{
    public abstract class TokenBase
    {
        protected readonly ISqlImplementation Implementation;

        protected TokenBase(ISqlImplementation implementation)
        {
            Implementation = implementation;
        }

        internal abstract bool IsAvailable(string[] availableFields, StringBuilder sb);
        public abstract string Build();
    }
}