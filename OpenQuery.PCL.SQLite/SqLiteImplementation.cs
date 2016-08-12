using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQuery.PCL.Abstract;

namespace OpenQuery.PCL.SQLite
{
    public class SqLiteImplementation : ISqlImplementation
    {
        public string Or => "OR";
        public string And => "AND";
        public string Like => "LIKE";
        public string Lesser => "<";
        public string IsEqual => "=";
        public string In => "IN";
        public string NotIn => "IN";
        public string Greater => ">";
        public string WildCard => "*";
        public string Select => "SELECT";
        public string Where => "WHERE";
        public string From => "FROM";
        public string FieldsSeparator => ", ";
        public string WhiteSpace => " ";
        public string OpenSubquery => "(";
        public string CloseCloseSubquery => ")";

        public StringBuilder CreateIn<T>(T[] value)
        {
            var sb = new StringBuilder();
            sb.Append(OpenSubquery).Append(string.Join(FieldsSeparator, value)).Append(CloseCloseSubquery);
            return sb;
        }

        public string JoinFields(List<string> fields)
        {
            return string.Join(FieldsSeparator, fields);
        }
    }
}
