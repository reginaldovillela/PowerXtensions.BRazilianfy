using System;
//using PowerXtensions.DotNet;
using PowerXtensions.BRazilianfy.Helpers;
using PowerXtensions.BRazilianfy.Extensions;

namespace PowerXtensions.BRazilianfy
{
    public struct Cnpj : IDocument<Cnpj>
    {
        private static readonly ushort[] multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly ushort[] multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        private string _unmasked { get; set; } = "";

        private string _masked { get; set; } = "";

        public Cnpj(string cnpjNumber)
        {
            _unmasked = cnpjNumber.OnlyNumbers();
            _masked = Convert.ToUInt64(_unmasked).ToString(@"000\.000\.000\-00"); ;
        }

        public bool IsValid()
        {
            if (_unmasked.Length != 14)
                return false;

            if (_unmasked.AllCharactersSame())
                return false;

            var digit1 = ModuleEleven.Calculate(_unmasked.Substring(0, 12), multiplier1);
            var digit2 = ModuleEleven.Calculate(_unmasked.Substring(0, 13), multiplier2);

            if (!_unmasked[12].ToInt().Equals(digit1))
                return false;

            if (!_unmasked[13].ToInt().Equals(digit2))
                return false;

            return true;
        }

        public static Cnpj GenerateRandom()
        {
            var cnpj = new Random().NextInt64(11111111111111, 99999999999999);
            var digit1 = ModuleEleven.Calculate($"{cnpj}", multiplier1);
            var digit2 = ModuleEleven.Calculate($"{cnpj}{digit1}", multiplier2);

            return new Cnpj($"{cnpj}{digit1}{digit2}");
        }

        public static bool IsValid(string cnpjNumber)
        {
            var cnpj = new Cnpj(cnpjNumber);

            return cnpj.IsValid();
        }
    }
}