using System.Text;

namespace OpenQuery.Core.Abstract
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
        string WhiteSpace { get; }
        string OpenSubquery { get; }
        string CloseCloseSubquery { get; }
        StringBuilder CreateIn<T>(T[] value);
        string JoinFields(IReadOnlyCollection<string> fields);
    }
}