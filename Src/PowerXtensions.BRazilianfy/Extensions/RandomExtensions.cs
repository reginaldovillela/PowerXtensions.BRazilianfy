using System;
using System.Text;

namespace PowerXtensions.BRazilianfy.Extensions;

/// <summary>
/// 
/// </summary>
public static class RandomExtensions
{ 
    internal static long NextLong(this Random value)
    {
        return value.NextLong(0, long.MaxValue);
    }

    internal static long NextLong(this Random value, long maxValue)
    {
        return value.NextLong(0, maxValue);
    }

    internal static long NextLong(this Random value, long minValue, long maxValue)
    {
        if (minValue > maxValue)
            throw new ArgumentOutOfRangeException(nameof(minValue), "The minValue is greater than maxValue.");

        if (minValue < 0)
            throw new ArgumentOutOfRangeException(nameof(minValue), "The minValue is less than zero.");

        var sb = new StringBuilder();
        var maxValueString = maxValue.ToString();
        var minValueString = minValue.ToString().PadLeft(maxValueString.Length, '0');

        for (var i = 0; i < maxValueString.Length; i++)
            sb.Append(value.Next(minValueString[i].ToInt(), maxValueString[i].ToInt()));

        return sb.ToString().ToLong();
    }
}