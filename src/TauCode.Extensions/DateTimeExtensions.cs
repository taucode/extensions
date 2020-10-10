using System;

namespace TauCode.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsExactDate(this DateTime dateTime)
        {
            return dateTime.Date == dateTime;
        }

        public static string ToExactDateString(this DateTime dateTime)
        {
            if (!dateTime.IsExactDate())
            {
                throw new ArgumentException("Not exact date", nameof(dateTime));
            }

            return dateTime.ToString("yyyy-MM-dd");
        }

        public static DateTimeOffset ToUtcDayOffset(this string timeString)
        {
            var time = DateTimeOffset.Parse(timeString);
            if (time.Offset != TimeSpan.Zero)
            {
// todo:wrong. zero day time - it is not so.
                throw new ArgumentException($"'{timeString}' does not represent a UTC date with zero day time.", nameof(timeString));
            }

            return time;
        }

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

    }
}
