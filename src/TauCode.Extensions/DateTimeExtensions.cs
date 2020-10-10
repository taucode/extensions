using System;

namespace TauCode.Extensions
{
    public static class DateTimeExtensions
    {
        public static readonly TimeSpan Day = TimeSpan.FromDays(1);

        public static TimeSpan AddDays(this TimeSpan timeSpan, double days)
        {
            return timeSpan.Add(TimeSpan.FromDays(days));
        }

        public static TimeSpan AddHours(this TimeSpan timeSpan, double hours)
        {
            return timeSpan.Add(TimeSpan.FromHours(hours));
        }

        public static TimeSpan AddMinutes(this TimeSpan timeSpan, double minutes)
        {
            return timeSpan.Add(TimeSpan.FromMinutes(minutes));
        }

        public static TimeSpan AddSeconds(this TimeSpan timeSpan, double seconds)
        {
            return timeSpan.Add(TimeSpan.FromSeconds(seconds));
        }

        public static TimeSpan AddMilliseconds(this TimeSpan timeSpan, double milliseconds)
        {
            return timeSpan.Add(TimeSpan.FromMilliseconds(milliseconds));
        }

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

        public static bool IsDayTime(this TimeSpan timeSpan)
        {
            return timeSpan.IsBetween(TimeSpan.Zero, Day);
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

        public static TimeSpan Min(TimeSpan v1, TimeSpan v2)
        {
            if (v1 < v2)
            {
                return v1;
            }

            return v2;
        }

        public static TimeSpan Max(TimeSpan v1, TimeSpan v2)
        {
            if (v1 > v2)
            {
                return v1;
            }

            return v2;
        }

        public static TimeSpan MinMax(
            TimeSpan min,
            TimeSpan max,
            TimeSpan v)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(max));
            }

            if (v <= min)
            {
                v = min;
            }

            if (v >= max)
            {
                v = max;
            }

            return v;
        }
    }
}
