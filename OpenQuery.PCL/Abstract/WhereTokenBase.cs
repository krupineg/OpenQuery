using System;
using System.Text;

namespace OpenQuery.PCL.Abstract
{
    internal abstract class WhereTokenBase<T> : TokenBase
    {
        private readonly string _name;
        private readonly T _val;

        internal WhereTokenBase(ISqlImplementation implementation, string name, T val) : base(implementation)
        {
            _name = name;
            _val = val;
        }

        public abstract StringBuilder GetSign();

        public override string Build()
        {
            return new StringBuilder(_name).Append(GetSign()).Append(_val).ToString();
        }

        internal override bool IsAvailable(string[] availableFilds, StringBuilder sb)
        {
            return Array.IndexOf(availableFilds, _name) > -1;
        }
    }
}