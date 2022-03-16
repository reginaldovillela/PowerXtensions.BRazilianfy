using System;
using PowerXtensions.BRazilianfy.Extensions;

namespace PowerXtensions.BRazilianfy.Helpers
{
    internal class Module11
    {
        internal static int Calculate(string value, params ushort[] multipliers)
        {
            if (value.LengthNotEqualTo(multipliers.Length))
                throw new Exception("Invalid data to calculate the digit");

            var i = 0;
            var amount = 0;

            foreach (var c in value)
            {
                amount += (c - '0') * multipliers[i];
                i++;
            }

            if ((amount % 11) < 2)
                return 0;
            
            return 11 - (amount % 11);
        }
    }
}