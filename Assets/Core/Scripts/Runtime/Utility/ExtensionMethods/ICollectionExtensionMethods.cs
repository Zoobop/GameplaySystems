using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility.ExtensionMethods
{
    public static class ICollectionExtensionMethods
    {
        public static bool IsEmpty<T>(this ICollection<T> items)
        {
            return items is {Count: 0};
        }

        public static void AddRange<T>(this ICollection<T> items, in ICollection<T> other)
        {
            if (other.IsEmpty()) return;

            foreach (var element in other)
            {
                items.Add(element);
            }
        }

        public static bool IsIdentical<T>(this ICollection<T> items, in ICollection<T> other)
        {
            // Check lengths
            if (items.Count != other.Count) return false;

            // Check each element
            for (var i = 0; i < items.Count; i++)
            {
                if (!Equals(items.ElementAt(i), other.ElementAt(i)))
                {
                    return false;
                }
            }

            // If this is reached, collections are identical
            return true;
        }

        public static bool RemoveIf<T>(this ICollection<T> items, Predicate<T> match)
        {
            foreach (var item in items)
            {
                if (match(item))
                {
                    return items.Remove(item);
                }
            }

            return false;
        }

        public static bool TryAdd<T>(this ICollection<T> items, in T itemToAdd)
        {
            var result = items.Contains(itemToAdd);
            if (!result)
            {
                items.Add(itemToAdd);
            }

            return !result;
        }
    }
}