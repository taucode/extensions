using System;
using System.Linq;

namespace TauCode.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsIn<T>(this T instance, params T[] values)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return values.Contains(instance);
        }

        public static bool IsBetween<T>(this T instance, T from, T to, bool inclusive = true) where T : IComparable<T>
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            bool isBetween =
                (instance.CompareTo(from) > 0 || (instance.CompareTo(from) == 0 && inclusive)) &&
                (instance.CompareTo(to) < 0 || (instance.CompareTo(to) == 0 && inclusive));

            return isBetween;
        }
    }
}
