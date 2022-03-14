using System;
//using System.Diagnostics;
//using PowerXtensions.DotNet;
using PowerXtensions.BRazilianfy.Extensions;
using PowerXtensions.BRazilianfy.Helpers;

namespace PowerXtensions.BRazilianfy
{
    /// <summary>
    /// CPF is Citizen's ID of Brazil. Like Social ID of U.S.A.
    /// </summary>
    public struct Cpf : IDocument<Cpf>
    {
        private static readonly ushort[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly ushort[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        private string _unmasked { get; set; } = "";

        private string _masked { get; set; } = "";

        public Cpf(string cpfNumber)
        {
            _unmasked = cpfNumber.OnlyNumbers();
            _masked = Convert.ToUInt64(_unmasked).ToString(@"000\.000\.000\-00"); ;
        }

        public override string ToString()
        {
            return ToStringMasked();
        }

        public string ToStringMasked()
        {
            return _masked;
        }

        public string ToStringUnmasked()
        {
            return _unmasked;
        }

        public bool IsValid()
        {
            if (_unmasked.Length != 11)
                return false;

            if (_unmasked.AllCharactersSame())
                return false;

            var digit1 = ModuleEleven.Calculate(_unmasked.Substring(0, 9), multiplier1);
            var digit2 = ModuleEleven.Calculate(_unmasked.Substring(0, 10), multiplier2);

            if (!_unmasked[9].ToInt().Equals(digit1))
                return false;

            if (!_unmasked[10].ToInt().Equals(digit2))
                return false;

            return true;
        }

        public static Cpf GenerateRandom()
        {
            var cpf = new Random().Next(111111111, 999999999);
            var digit1 = ModuleEleven.Calculate($"{cpf}", multiplier1);
            var digit2 = ModuleEleven.Calculate($"{cpf}{digit1}", multiplier2);

            return new Cpf($"{cpf}{digit1}{digit2}");
        }

        public static bool IsValid(string cpfNumber)
        {
            var cpf = new Cpf(cpfNumber);

            return cpf.IsValid();
        }
    }
}