using System;

namespace TauCode.Extensions
{
    public static class TimeSpanExtensions
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

        public static bool IsDayTime(this TimeSpan timeSpan)
        {
            return timeSpan.IsBetween(TimeSpan.Zero, Day);
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
