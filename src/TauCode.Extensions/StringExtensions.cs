using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace TauCode.Extensions
{
    public static class StringExtensions
    {
        public static TEnum ToEnum<TEnum>(this string s, bool ignoreCase = false) where TEnum : struct
        {
            var res = (TEnum)Enum.Parse(typeof(TEnum), s, ignoreCase);
            return res;
        }

        public static bool ToBoolean(this string s)
        {
            return bool.Parse(s);
        }

        public static int ToInt32(this string s)
        {
            return int.Parse(s, CultureInfo.InvariantCulture);
        }

        public static long ToInt64(this string s)
        {
            return long.Parse(s, CultureInfo.InvariantCulture);
        }

        public static float ToFloat(this string s)
        {
            return float.Parse(s, CultureInfo.InvariantCulture);
        }

        public static double ToDouble(this string s)
        {
            return double.Parse(s, CultureInfo.InvariantCulture);
        }

        public static decimal ToDecimal(this string s)
        {
            return decimal.Parse(s, CultureInfo.InvariantCulture);
        }

        public static DateTime ToExactDate(this string s)
        {
            string pattern = @"^(\d\d\d\d)-(\d\d)-(\d\d)$";

            Match m = Regex.Match(s, pattern);
            if (m.Success)
            {
                int year = m.Result("$1").ToInt32();
                int month = m.Result("$2").ToInt32();
                int day = m.Result("$3").ToInt32();

                DateTime result = new DateTime(year, month, day);
                return result;
            }
            else
            {
                throw new FormatException("Expected format is 'yyyy-MM-dd'");
            }
        }

        public static Guid ToGuid(this string s)
        {
            return Guid.Parse(s);
        }

        // todo ut this
        public static DateTimeOffset ToUtcDateOffset(this string timeString)
        {
            if (timeString == null)
            {
                throw new ArgumentNullException(nameof(timeString));
            }

            var time = DateTimeOffset.Parse(timeString);
            var valid = time.IsUtcDateOffset();

            if (!valid)
            {
                throw new ArgumentException(
                    $"'{timeString}' does not represent a UTC date with zero day time.",
                    nameof(timeString));
            }

            return time;
        }

        public static DateTimeOffset? ToNullableUtcDateOffset(this string timeString) =>
            timeString?.ToUtcDateOffset();

        public static string[] GetLines(this string text)
        {
            return text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public static string CutEmptyLines(this string text)
        {
            var cutLines = text
                .GetLines()
                .Select(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return string.Empty;
                    }

                    return x;
                })
                .ToArray();

            var cutText = string.Join(Environment.NewLine, cutLines);

            return cutText;
        }

        public static bool StartsWithEmptyChar(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (s == string.Empty)
            {
                return false;
            }
            else
            {
                return char.IsWhiteSpace(s[0]);
            }
        }

        public static bool EndsWithEmptyChar(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (s == string.Empty)
            {
                return false;
            }
            else
            {
                return char.IsWhiteSpace(s[^1]);
            }
        }

        public static bool ContainsAnyOf(this string s, params char[] chars)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return s.Any(c => c.IsIn(chars));
        }

        public static bool EndsWithAnyOf(this string s, params char[] chars)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (s == string.Empty)
            {
                return false;
            }
            else
            {
                return s.Last().IsIn(chars);
            }
        }

        public static bool StartsWithAnyOf(this string s, params char[] chars)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (s == string.Empty)
            {
                return false;
            }
            else
            {
                return s.First().IsIn(chars);
            }
        }

        public static bool StartsWithDigit(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return s != string.Empty && char.IsDigit(s[0]);
        }

        public static bool StartsWithLatinLetter(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return s != string.Empty && s[0].IsLatinLetter();
        }
    }
}
