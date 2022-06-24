using System;
using System.Collections.Generic;
using System.Linq;

namespace TauCode.Extensions
{
    public static class CollectionExtensions
    {
        #region Find First Index

        private static int FindFirstIndexInList<T>(
            this IList<T> list,
            Func<T, bool> predicate,
            int startPosition = 0)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (startPosition < 0 || startPosition > list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(startPosition));
            }

            for (var i = startPosition; i < list.Count; i++)
            {
                var item = list[i];
                if (predicate(item))
                {
                    return i;
                }
            }

            return -1;
        }

        private static int FindFirstIndexInReadOnlyList<T>(
            this IReadOnlyList<T> list,
            Func<T, bool> predicate,
            int startPosition = 0)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (startPosition < 0 || startPosition > list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(startPosition));
            }

            for (var i = startPosition; i < list.Count; i++)
            {
                var item = list[i];
                if (predicate(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int FindFirstIndex<T>(
            this IEnumerable<T> collection,
            Func<T, bool> predicate,
            int startPosition = 0)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection is IList<T> list)
            {
                return FindFirstIndexInList(list, predicate, startPosition);
            }

            if (collection is IReadOnlyList<T> readOnlyList)
            {
                return FindFirstIndexInReadOnlyList(readOnlyList, predicate, startPosition);
            }

            throw new ArgumentException(
                $"'{nameof(collection)}' must be either IList<T> or IReadOnlyList<T>.",
                nameof(collection));
        }

        public static int FindFirstIndex<T>(this IEnumerable<T> collection, T value, int startPosition = 0) =>
            collection.FindFirstIndex(x => Equals(x, value), startPosition);

        #endregion

        #region Find Last Index

        private static int FindLastIndexInList<T>(
            this IList<T> list,
            Func<T, bool> predicate,
            int startPosition = 0)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (startPosition < 0 || startPosition > list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(startPosition));
            }

            for (var i = list.Count - 1; i >= startPosition; i--)
            {
                var item = list[i];
                if (predicate(item))
                {
                    return i;
                }
            }

            return -1;
        }

        private static int FindLastIndexInReadOnlyList<T>(
            this IReadOnlyList<T> list,
            Func<T, bool> predicate,
            int startPosition = 0)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (startPosition < 0 || startPosition > list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(startPosition));
            }

            for (var i = list.Count - 1; i >= startPosition; i--)
            {
                var item = list[i];
                if (predicate(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int FindLastIndex<T>(
            this IEnumerable<T> collection,
            Func<T, bool> predicate,
            int startPosition = 0)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection is IList<T> list)
            {
                return FindLastIndexInList(list, predicate, startPosition);
            }

            if (collection is IReadOnlyList<T> readOnlyList)
            {
                return FindLastIndexInReadOnlyList(readOnlyList, predicate, startPosition);
            }

            throw new ArgumentException(
                $"'{nameof(collection)}' must be either IList<T> or IReadOnlyList<T>.",
                nameof(collection));
        }

        public static int FindLastIndex<T>(this IEnumerable<T> collection, T value, int startPosition = 0) =>
            collection.FindLastIndex(x => Equals(x, value), startPosition);

        #endregion

        public static TValue GetDictionaryValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (dictionary is Dictionary<TKey, TValue> dictionaryImplementation)
            {
                return dictionaryImplementation.GetValueOrDefault(key);
            }

            throw new ArgumentException($"'{nameof(dictionary)}' is not a 'Dictionary<TKey, TValue>'.",
                nameof(dictionary));
        }

        public static void AddCharRange(this List<char> list, char from, char to)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (from > to)
            {
                throw new ArgumentOutOfRangeException(nameof(to),
                    $"'{nameof(to)}' must be not less than '{nameof(from)}'.");
            }

            list.AddRange(Enumerable.Range(from, to - from + 1).Select(x => (char) x));
        }

        public static bool ListsAreEquivalent<T>(IReadOnlyList<T> list1, IReadOnlyList<T> list2, bool sort = true)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            IList<T> transformedList1 = list1.ToList();
            IList<T> transformedList2 = list2.ToList();

            if (sort)
            {
                transformedList1 = list1.OrderBy(x => x).ToList();
                transformedList2 = list2.OrderBy(x => x).ToList();
            }

            for (var i = 0; i < transformedList1.Count; i++)
            {
                var v1 = transformedList1[i];
                var v2 = transformedList2[i];

                if (v1 == null)
                {
                    if (v2 == null)
                    {
                        // ok
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var eq = v1.Equals(v2);
                    if (!eq)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
