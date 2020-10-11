using System;

namespace TauCode.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsDate(this DateTime date)
        {
            return date.TimeOfDay == TimeSpan.Zero;
        }

        public static string ToDateString(this DateTime date)
        {
            if (!date.IsDate())
            {
                throw new ArgumentException($"Not an exact date: '{date}'.", nameof(date));
            }

            return date.ToString("yyyy-MM-dd");
        }
    }
}
