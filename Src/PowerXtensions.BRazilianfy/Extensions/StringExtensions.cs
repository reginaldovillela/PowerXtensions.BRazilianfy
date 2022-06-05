using System;
using System.Linq;
using System.Text;

namespace PowerXtensions.BRazilianfy.Extensions;

/// <summary>
/// 
/// </summary>
internal static class StringExtensions
{
    /// <summary>
    /// Checks if all characters are the same
    /// </summary>
    /// <param name="value"></param>
    /// <returns>True if all are the same</returns>
    internal static bool AllCharactersEquals(this string value)
    {
        if (value.Length <= 1)
            return true;

        var charToCompare = value[0];

        for (var i = 0; i < value.Length; i++)
            if (value[i] != charToCompare)
                return false;
            else
                charToCompare = value[i];

        return true;
    }

    /// <summary>
    /// Removes all non-number characters
    /// </summary>
    /// <param name="value">String for analysis</param>
    /// <returns>Returns a String with numbers only</returns>
    internal static string OnlyNumbers(this string value)
    {
        var charsNumbers = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        var sb = new StringBuilder();

        for (var i = 0; i < value.Length; i++)
            if (charsNumbers.Contains(value[i]))
                sb.Append(value[i]);

        return sb.ToString();
    }

    /// <summary>
    /// Compares the string length with the given value. Checks if the length is equal.
    /// </summary>
    /// <param name="value">String to compare</param>
    /// <param name="lengthCompare">Length to compare</param>
    /// <returns>Returns true if the length is equal</returns>
    public static bool LengthEqualTo(this string value, int lengthCompare)
        => value.Length == lengthCompare;

    /// <summary>
    /// Compares the string length to the given value. Checks if the length is different.
    /// </summary>
    /// <param name="value">String to compare</param>
    /// <param name="lengthCompare">Length to compare</param>
    /// <returns>Returns true if the length is not equal</returns>
    public static bool LengthNotEqualTo(this string value, int lengthCompare)
        => value.Length != lengthCompare;

    /// <summary>
    /// Converts the String to a Long. If unable to convert an exception will be thrown
    /// </summary>
    /// <param name="value">String to be converted</param>
    /// <returns>A Long will be returned or an exception will be thrown</returns>
    public static long ToLong(this string value)
        => long.TryParse(value, out var result)
        ? result
        : throw new InvalidCastException($"Unable to convert the {value} value to a long");
}