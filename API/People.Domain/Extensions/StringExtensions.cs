namespace People.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumericOnly(this string s) => s.All(char.IsDigit);
    }
}
