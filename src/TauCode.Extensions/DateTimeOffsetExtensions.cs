using System;

namespace TauCode.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset Min(DateTimeOffset v1, DateTimeOffset v2)
        {
            if (v1 < v2)
            {
                return v1;
            }

            return v2;
        }

        public static DateTimeOffset Max(DateTimeOffset v1, DateTimeOffset v2)
        {
            if (v1 > v2)
            {
                return v1;
            }

            return v2;
        }

        public static bool IsUtcDateOffset(this DateTimeOffset date) => date.UtcDateTime.TimeOfDay == TimeSpan.Zero;

        public static string ToUtcDateOffsetString(this DateTimeOffset date)
        {
            if (!date.IsUtcDateOffset())
            {
                throw new ArgumentException($"Not an exact UTC date offset: '{date}'.", nameof(date));
            }

            return date.ToString("yyyy-MM-ddZ");
        }
    }
}
