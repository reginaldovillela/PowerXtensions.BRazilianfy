using System;
using PowerXtensions.BRazilianfy.Contracts;
using PowerXtensions.BRazilianfy.Extensions;
using PowerXtensions.BRazilianfy.Helpers;

namespace PowerXtensions.BRazilianfy
{
    /// <summary>
    /// CPF is Citizen's ID of Brazil. Like Social ID of U.S.A.
    /// The CPF consists of 11 digits and is issued by the RFB (Receita Federal do Brasil)
    /// </summary>
    public readonly struct Cpf : IDocument<Cpf>
    {
        private const int DefaultLenght = 11;

        private const string DefaultMask = @"000\.000\.000\-00";

        private static readonly ushort[] Multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        private static readonly ushort[] Multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        private readonly string _cpfNumber;

        private string Masking => _cpfNumber.OnlyNumbers().ToLong().Mask(DefaultMask);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpfNumber"></param>
        public Cpf(string cpfNumber)
        {
            _cpfNumber = cpfNumber;
        }

        /// <summary>
        /// Checks if the CPF number is valid. Obs.: Validation only of the CPF composition and not if it is a real CPF.
        /// </summary>
        /// <returns>Return if valid</returns>
        public bool IsValid()
        {
            var number = _cpfNumber.OnlyNumbers();

            if (number.LengthNotEqualTo(DefaultLenght))
                return false;

            if (number.AllCharactersSame())
                return false;

            var digit1 = Module11.Calculate(number[..9], Multiplier1);
            var digit2 = Module11.Calculate(number[..10], Multiplier2);

            if (!number[9].ToInt().Equals(digit1))
                return false;

            if (!number[10].ToInt().Equals(digit2))
                return false;

            return true;
        }
        
        /// <summary>
        /// Returns a string with the CPF number, clean, validated and with the mask 000.000.000-00
        /// </summary>
        /// <returns>Returns CPF number with the mask</returns>
        public string ToStringWithMask()
        {
            return IsValid() 
                ? Masking 
                : "Not a valid CPF";
        }

        /// <summary>
        /// Returns a string with the CPF number, clean, validated and without the mask.
        /// </summary>
        /// <returns>Returns CPF number without the mask</returns>
        public string ToStringWithoutMask()
        {
            return IsValid() 
                ? _cpfNumber.OnlyNumbers() 
                : "Not a valid CPF";
        }
        
        /// <summary>
        /// Returns a string with the CPF number and a message.
        /// </summary>
        /// <returns>Returns CPF number</returns>
        public override string ToString()
        {
            return IsValid() 
                ? $"The {Masking} is a valid CPF" 
                : $"The {_cpfNumber} is a invalid CPF";
        }
        
        #region Static Methods

        public static Cpf GenerateRandom()
        {
            var cpf = new Random().Next(111111111, 999999999);
            var digit1 = Module11.Calculate($"{cpf}", Multiplier1);
            var digit2 = Module11.Calculate($"{cpf}{digit1}", Multiplier2);

            return new Cpf($"{cpf}{digit1}{digit2}");
        }

        public static bool IsValid(string cpfNumber)
        {
            return new Cpf(cpfNumber).IsValid();
        }

        #endregion
    }
}