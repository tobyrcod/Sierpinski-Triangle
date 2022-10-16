using System.Collections.Generic;
using System.Text;

namespace ExtensionMethods
{
    public static class ListExtensions
    {
        public static string BuildString<T>(this IReadOnlyList<T> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < list.Count; i++)
            {
                if (i != 0)
                    sb.Append(", ");

                T t = list[i];
                sb.Append($"{t}");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}