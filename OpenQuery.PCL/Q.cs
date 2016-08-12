using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQuery.PCL.Abstract;
using OpenQuery.PCL.Tokens;

namespace OpenQuery.PCL
{
    internal class Q<TImplementation> :
        IHaveWhereClause, IQuery, IWhereQuery, IAvailableWhereQuery, IAvailableNewWhereClause, ISelectedQueryHidden, IQueryBaseHidden, IQueryHidden
        where TImplementation: ISqlImplementation, new()
    {
        public string Query
        {
            get
            {
                if (string.IsNullOrEmpty(_query))
                {
                    _query = Build();
                }
                return _query;
            }
        }

        private readonly TImplementation _implementation;
        private string _table = string.Empty;
        private readonly ICollection<TokenBase> _whereTokens = new List<TokenBase>();
        private List<string> _fields = new List<string>();
        private string[] _availableFilds = new string[0];
        private string _query;

        public Q()
        {
            _implementation = new TImplementation();
        }

        public ISelectedQuery Select(IList<string> fields)
        {
            _fields = new HashSet<string>(fields).ToList();
            return this;
        }

        public ISelectedQuery Select(string fields)
        {
            _fields = new HashSet<string>(fields.Split(',').Select(x => x.Trim())).ToList();
            return this;
        }

        public ISelectedQuery Select(params string[] fields)
        {
            if (!fields.Any())
            {
                return Select(_implementation.WildCard);
            }
            else
            {
                return Select(fields.ToList());
            }
        }
        
        public IAvailableWhereQuery From<T>()
        {
            var type = typeof (T);
            return From(type);
        }

        public IAvailableWhereQuery From(Type type)
        {
            _table = type.Name;
            _availableFilds = QueryFieldsCache.GetProperties(type);
            return this;
        }

        public IQuery Where(TokenBase where)
        {
            _whereTokens.Add(where);
            return this;
        }

        public IQuery AndWhere()
        {
            _whereTokens.Add(new And(_implementation));
            return this;
        }

        public IQuery OrWhere()
        {
            _whereTokens.Add(new Or(_implementation));
            return this;
        }


        public ISqlImplementation Implementation => _implementation;

        public string Build()
        {
            _fields = _fields.Intersect(_availableFilds).ToList();
            if (!_fields.Any())
            {
                _fields.Add(_implementation.WildCard);
            }
            var sb = new StringBuilder();
            sb
                .Append(_implementation.Select).Append(_implementation.WhiteSpace)
                .Append(_implementation.JoinFields(_fields))
                .Append(_implementation.WhiteSpace)
                .Append(_implementation.From).Append(_implementation.WhiteSpace)
                .Append(_table);
            sb.Append(GetWhereStringBuilder());
            _query = sb.ToString();
            return Query;
        }

        private StringBuilder GetWhereStringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            if (_whereTokens.Any())
            {
                foreach (var whereToken in _whereTokens)
                {
                    if (whereToken.IsAvailable(_availableFilds, sb))
                    {
                        sb.Append(_implementation.WhiteSpace).Append(whereToken.Build());
                    }
                }
            }
            if (sb.Length > 0)
            {
                sb.Insert(0, _implementation.Where).Insert(0, _implementation.WhiteSpace);
            }
            return sb;
        }
    }
}