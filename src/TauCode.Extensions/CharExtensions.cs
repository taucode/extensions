namespace TauCode.Extensions
{
    public static class CharExtensions
    {
        public static bool IsLatinLetter(this char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }
    }
}
