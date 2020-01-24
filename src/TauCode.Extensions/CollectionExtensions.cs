using System;
using System.Collections.Generic;
using System.Linq;

namespace TauCode.Extensions
{
    public static class CollectionExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return dictionary.TryGetValue(key, out var value) ?
                value :
                default;
        }

        public static TValue GetOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return dictionary.TryGetValue(key, out var value) ?
                value :
                default;
        }

        #region Find Index for IList<T>

        public static int FindFirstIndexOf<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (predicate(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int FindFirstIndexOf<T>(this IList<T> list, T value)
        {
            return list.FindFirstIndexOf(x => Equals(x, value));
        }

        public static int FindLastIndexOf<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (var i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];
                if (predicate(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int FindLastIndexOf<T>(this IList<T> list, T value)
        {
            return list.FindLastIndexOf(x => Equals(x, value));
        }

        #endregion

        #region Find Index for IReadOnlyList<T>

        public static int FindFirstIndexOf<T>(this IReadOnlyList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (predicate(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int FindFirstIndexOf<T>(this IReadOnlyList<T> list, T value)
        {
            return list.FindFirstIndexOf(x => Equals(x, value));
        }

        public static int FindLastIndexOf<T>(this IReadOnlyList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (var i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];
                if (predicate(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int FindLastIndexOf<T>(this IReadOnlyList<T> list, T value)
        {
            return list.FindLastIndexOf(x => Equals(x, value));
        }

        #endregion

        public static void AddCharRange(this List<char> list, char from, char to)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (from > to)
            {
                throw new ArgumentOutOfRangeException(nameof(to), $"'{nameof(to)}' must be not less than '{nameof(from)}'.");
            }

            list.AddRange(Enumerable.Range(from, to - from + 1).Select(x => (char)x));
        }
    }
}
