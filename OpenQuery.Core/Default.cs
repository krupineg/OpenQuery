using System.Text;
using OpenQuery.Core.Abstract;

namespace OpenQuery.Core;

public class Default : ISqlDialect
{
    public string Or => "OR";
    public string And => "AND";
    public string Like => "LIKE";
    public string Less => "<";
    public string IsEqual => "=";
    public string In => "IN";
    public string NotIn => "NOT IN";
    public string Greater => ">";
    public string WildCard => "*";
    public string Select => "SELECT";
    public string Where => "WHERE";
    public string From => "FROM";
    public string Count => "COUNT";
    public string FieldsSeparator => ", ";
    public string DomainSeparator => ".";
    public string WhiteSpace => " ";
    public string OpenSubquery => "(";
    public string CloseSubquery => ")";
    public string Alias => "AS";
    public string Limit => "LIMIT";
    public string Offset => "OFFSET";

    public StringBuilder CreateIn<T>(T[] value)
    {
        var sb = new StringBuilder();
        sb.Append(OpenSubquery).Append(string.Join(FieldsSeparator, value)).Append(CloseSubquery);
        return sb;
    }

    public string JoinFields(IReadOnlyCollection<string> fields)
    {
        return string.Join(FieldsSeparator, fields);
    }
}