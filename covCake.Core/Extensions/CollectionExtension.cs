using System.Collections.Generic;
using System.Diagnostics;

namespace covCake
{
    [DebuggerStepThrough]
    public static class CollectionExtension
    {
     
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return (collection == null) || (collection.Count == 0);
        }
    }
}