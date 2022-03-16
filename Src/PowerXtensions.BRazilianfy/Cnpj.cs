using System;
using PowerXtensions.BRazilianfy.Contracts;
using PowerXtensions.BRazilianfy.Helpers;
using PowerXtensions.BRazilianfy.Extensions;

namespace PowerXtensions.BRazilianfy
{
    public readonly struct Cnpj : IDocument<Cnpj>
    {
        private const int DefaultLenght = 14;

        private const string DefaultMask = @"00\.000\.000\/0000\-00";

        private static readonly ushort[] Multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        private static readonly ushort[] Multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        private readonly string _cnpjNumber;

        private string Masking => _cnpjNumber.OnlyNumbers().ToLong().Mask(DefaultMask);
        
        public Cnpj(string cnpjNumber)
        {
            _cnpjNumber = cnpjNumber;
        }

        public bool IsValid()
        {
            var number = _cnpjNumber.OnlyNumbers();

            if (number.LengthNotEqualTo(DefaultLenght))
                return false;

            if (number.AllCharactersSame())
                return false;

            var digit1 = Module11.Calculate(number[..12], Multiplier1);
            var digit2 = Module11.Calculate(number[..13], Multiplier2);

            if (!number[12].ToInt().Equals(digit1))
                return false;

            if (!number[13].ToInt().Equals(digit2))
                return false;

            return true;
        }
        
        public string ToStringWithMask()
        {
            return IsValid() 
                ? Masking
                : "Not a valid CNPJ";
        }

        public string ToStringWithoutMask()
        {
            return IsValid() 
                ? _cnpjNumber.OnlyNumbers() 
                : "Not a valid CNPJ";
        }

        public override string ToString()
        {
            return IsValid() 
                ? $"The {Masking} is a valid CNPJ" 
                : $"The {_cnpjNumber} is a invalid CNPJ";
        }

        #region Static Methods
        
        public static Cnpj GenerateRandom()
        {
            var cnpj = new Random().NextInt64(11111111111111, 99999999999999);
            var digit1 = Module11.Calculate($"{cnpj}", Multiplier1);
            var digit2 = Module11.Calculate($"{cnpj}{digit1}", Multiplier2);

            return new Cnpj($"{cnpj}{digit1}{digit2}");
        }

        public static bool IsValid(string cnpjNumber)
        {
            return new Cnpj(cnpjNumber).IsValid();
        }
        
        #endregion
    }
}