namespace TauCode.Extensions;

public static class MemoryExtensions
{
    public static IList<ReadOnlyMemory<T>> Split<T>(this ReadOnlySpan<T> input, params T[] separators)
        where T : struct
    {
        if (separators.Length == 0)
        {
            throw new ArgumentException($"'{nameof(separators)}' cannot be empty.", nameof(separators));
        }

        var list = new List<ReadOnlyMemory<T>>();
        var remainder = input;
        var pos = 0;

        while (true)
        {
            if (remainder.IsEmpty)
            {
                list.Add(new ReadOnlyMemory<T>(remainder.ToArray()));
                return list;
            }

            if (pos == remainder.Length)
            {
                list.Add(remainder.ToArray());
                return list;
            }

            var v = remainder[pos];
            if (separators.Contains(v))
            {
                var part = remainder[..pos];
                list.Add(part.ToArray());
                remainder = remainder[(pos + 1)..];
                pos = 0;
            }
            else
            {
                pos++;
            }
        }
    }
}