using System;
using PowerXtensions.BRazilianfy.Contracts;
using PowerXtensions.BRazilianfy.Extensions;
using PowerXtensions.BRazilianfy.Helpers;

namespace PowerXtensions.BRazilianfy
{
    public readonly struct Pis : IDocument<Pis>
    {
        private const int DefaultLenght = 11;

        private const string DefaultMask = @"000\.00000\.00\-0";

        private static readonly ushort[] Multiplier = { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        private readonly string _pisNumber;

        private string Masking => _pisNumber.OnlyNumbers().ToLong().Mask(DefaultMask);
        
        public Pis(string pisNumber)
        {
            _pisNumber = pisNumber;
        }

        public bool IsValid()
        {
            var number = _pisNumber.OnlyNumbers();

            if (number.LengthNotEqualTo(DefaultLenght))
                return false;

            if (number.AllCharactersSame())
                return false;

            var digit = Module11.Calculate(number[..10], Multiplier);

            if (!number[10].ToInt().Equals(digit))
                return false;
            
            return true;
        }
        
        public string ToStringWithMask()
        {
            return IsValid() 
                ? Masking
                : "Not a valid PIS";
        }

        public string ToStringWithoutMask()
        {
            return IsValid() 
                ? _pisNumber.OnlyNumbers() 
                : "Not a valid PIS";
        }
        
        public override string ToString()
        {
            return IsValid() 
                ? $"The {Masking} is a valid PIS" 
                : $"The {_pisNumber} is a invalid PIS";
        }

        #region Static Methods

        public static Pis GenerateRandom()
        {
            var pis = new Random().Next(111111111, 999999999);
            var digit1 = Module11.Calculate($"{pis}", Multiplier);

            return new Pis($"{pis}{digit1}");
        }

        public static bool IsValid(string pisNumber)
        {
            return new Pis(pisNumber).IsValid();
        }

        #endregion
    }
}