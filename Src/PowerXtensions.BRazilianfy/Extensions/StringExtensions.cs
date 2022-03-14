using System.Linq;
using System.Text;

namespace PowerXtensions.BRazilianfy.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Checks if all characters are the same
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if all are the same</returns>
        internal static bool AllCharactersSame(this string value)
        {
            if (value.Length <= 1)
                return true;

            var charToCompare = value[0];

            for (int i = 0; i < value.Length; i++)
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

            for (int i = 0; i < value.Length; i++)
                if (charsNumbers.Contains(value[i]))
                    sb.Append(value[i]);

            return sb.ToString();
        }
    }
}