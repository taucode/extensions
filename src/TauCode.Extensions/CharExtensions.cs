namespace TauCode.Extensions
{
    public static class CharExtensions
    {
        public static bool IsLatinLetter(this char c)
        {
            return c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
        }
    }
}
