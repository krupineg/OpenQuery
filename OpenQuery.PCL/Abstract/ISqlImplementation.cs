using System.Collections.Generic;
using System.Text;

namespace OpenQuery.PCL.Abstract
{
    public interface ISqlImplementation
    {
        string Or { get; }
        string And { get; }
        string Like { get; }
        string Lesser { get; }
        string IsEqual { get; }
        string In { get; }
        string NotIn { get; }
        string Greater { get; }
        string WildCard { get; }
        string Select { get; }
        string Where { get; }
        string From { get; }
        string FieldsSeparator { get; }
        string WhiteSpace { get; }
        string OpenSubquery { get; }
        string CloseCloseSubquery { get; }
        StringBuilder CreateIn<T>(T[] value);
        string JoinFields(List<string> fields);
    }
}