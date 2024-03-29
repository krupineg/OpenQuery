﻿using System.Text;

namespace OpenQuery.Core.Abstract.Dialect
{
    public interface ISqlDialect
    {
        string Or { get; }
        string And { get; }
        string Like { get; }
        string Less { get; }
        string IsEqual { get; }
        string In { get; }
        string NotIn { get; }
        string Greater { get; }
        string WildCard { get; }
        string Select { get; }
        string Where { get; }
        string From { get; }
        string Count { get; }
        string FieldsSeparator { get; }
        string DomainSeparator { get; }
        string WhiteSpace { get; }
        string OpenSubquery { get; }
        string CloseSubquery { get; }
        string Alias { get; }
        string Limit { get; }
        string Offset { get; }
        string QuoteValue(string value);
        StringBuilder CreateIn<T>(T[] value);
        string JoinFields(IReadOnlyCollection<string> fields);
    }
}