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

        public static bool IsUtcDateOffset(this DateTimeOffset date) =>
            date.UtcDateTime.TimeOfDay == TimeSpan.Zero &&
            date.Offset == TimeSpan.Zero;

        public static string ToUtcDateOffsetString(this DateTimeOffset date)
        {
            if (!date.IsUtcDateOffset())
            {
                throw new ArgumentException($"Not an exact UTC date offset: '{date}'.", nameof(date));
            }

            return date.ToString("yyyy-MM-ddZ");
        }

        public static DateTimeOffset CutDateOffset(this DateTimeOffset dateTimeOffset)
        {
            dateTimeOffset = dateTimeOffset.ToUniversalTime();

            return new DateTimeOffset(
                dateTimeOffset.Year,
                dateTimeOffset.Month,
                dateTimeOffset.Day,
                0,
                0,
                0,
                TimeSpan.Zero);
        }
    }
}
