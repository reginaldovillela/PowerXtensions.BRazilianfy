using System;

namespace PowerXtensions.BRazilianfy.Extensions;
internal static class CharExtensions
{
    internal static int ToInt(this char value)
    {
        if (!char.IsDigit(value))
            throw new InvalidCastException("The char is not a digit");

        return value - '0';
    }
}