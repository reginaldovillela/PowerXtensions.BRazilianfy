namespace PowerXtensions.BRazilianfy.Extensions
{
    internal static class LongExtensions
    {
        internal static string Mask(this long value, string mask)
            => value.ToString(mask);
    }
}