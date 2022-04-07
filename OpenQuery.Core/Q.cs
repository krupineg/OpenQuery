using System.Diagnostics.Contracts;
using System.Text;
using OpenQuery.Core.Abstract.Clauses.From;
using OpenQuery.Core.Abstract.Clauses.Select;
using OpenQuery.Core.Abstract.Clauses.Where;
using OpenQuery.Core.Abstract.Dialect;
using OpenQuery.Core.Abstract.Query;
using OpenQuery.Core.Abstract.Tokens;
using OpenQuery.Core.Clauses;
using OpenQuery.Core.Tokens;

namespace OpenQuery.Core
{
    internal class Q<TDialect> :
        IHaveWhereClause, 
        IFromQuery,
        IWhereQuery,
        IAvailableWhereQuery, 
        IAvailableNewWhereClause, 
        ISelectedQueryHidden,
        IQueryBaseHidden,
        IQueryHidden
        where TDialect: ISqlDialect, new()
    {
        private readonly ISelectClauseFactory _selectClauseFactory =
            new SelectClauseFactory(new FunctionCallClauseFactory());
        private readonly IWhereClauseFactory _whereClauseFactory =
            new WhereClauseFactory(new FunctionCallClauseFactory());

        private readonly IFromClauseFactory _fromClauseFactory = new FromClauseFactory();
        
        private readonly TDialect _dialect;
        private readonly List<FromExpression> _sourceExpressions = new();
        private readonly ICollection<IToken> _whereTokens = new List<IToken>();
        private readonly List<SelectExpression> _selectExpressions = new ();
        private readonly ISet<string> _alias = new HashSet<string>();
        private readonly ISet<long> _limits = new HashSet<long>();
        private readonly ISet<long> _offsets = new HashSet<long>();

        public Q()
        {
            _dialect = new TDialect();
        }
        
        public ISelectedQuery Select<T>(Func<ISelectClauseFactory, SelectExpression> func)
        {
            _selectExpressions.Add(func(_selectClauseFactory));
            return this;
        }
        
        public ISelectedQuery Select(Func<ISelectClauseFactory, SelectExpression> expression)
        {
            _selectExpressions.Add(expression(_selectClauseFactory));
            return this;
        }
        public IAvailableWhereQuery From(Func<IFromClauseFactory, FromExpression> func)
        {
            _sourceExpressions.Add(func(_fromClauseFactory)); 
            return this;
        }

        public IQuery Where(WhereFactoryExpression where)
        {
            _whereTokens.Add(where(_whereClauseFactory));
            return this;
        }

        public IQuery AndWhere()
        {
            _whereTokens.Add(new And());
            return this;
        }

        public IQuery OrWhere()
        {
            _whereTokens.Add(new Or());
            return this;
        }

        public IFromQuery As(string alias)
        {
            Contract.Assert(_alias.Count < 1, "there could be only one alias");
            _alias.Add(alias);
            return this;
        }


        public ISqlDialect Dialect => _dialect;

        public FromExpression Source => _sourceExpressions.Single();

        public string Build()
        {
            var sb = new StringBuilder();
                
            var (table, fields) = Source.Invoke(_dialect);
            
            sb.Append(_dialect.Select).Append(_dialect.WhiteSpace)
                .Append(_selectExpressions.Single().Invoke(_dialect, fields))
                .Append(_dialect.WhiteSpace)
                .Append(_dialect.From).Append(_dialect.WhiteSpace)
                .Append(table);
            
            Optional(_dialect.Alias, _alias);
            
            sb.Append(GetWhereStringBuilder());
            
            Optional(_dialect.Limit, _limits);
            Optional(_dialect.Offset, _offsets);

            void Optional<T>(string prefix, ISet<T> values)
            {
                foreach (var value in values)
                {
                    sb.Append($" {prefix} {value}");
                }
            }
            
            return sb.ToString();
        }

        private StringBuilder GetWhereStringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            if (_whereTokens.Any())
            {
                foreach (var whereToken in _whereTokens)
                {
                    sb.Append(_dialect.WhiteSpace).Append(whereToken.Build(_dialect));
                }
            }
            if (sb.Length > 0)
            {
                sb.Insert(0, _dialect.Where).Insert(0, _dialect.WhiteSpace);
            }
            return sb;
        }

        public IReadyToBuildQuery Limit(long limit)
        {
            Contract.Assert(_limits.Count < 1, "there could be only one limit per query");
            _limits.Add(limit);
            return this;
        }

        public IReadyToBuildQuery Offset(long offset)
        {
            Contract.Assert(_offsets.Count < 1, "there could be only one offset per query");
            _offsets.Add(offset);
            return this;
        }
    }
}