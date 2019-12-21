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
    }
}
