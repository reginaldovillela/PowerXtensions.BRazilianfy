using System;
using PowerXtensions.BRazilianfy.Contracts;
using PowerXtensions.BRazilianfy.Helpers;
using PowerXtensions.BRazilianfy.Extensions;

namespace PowerXtensions.BRazilianfy;

/// <summary>
/// 
/// </summary>
public readonly struct Cnpj : IDocument<Cnpj>
{
    private const int DefaultLenght = 14;

    private const string DefaultMask = @"00\.000\.000\/0000\-00";

    private static readonly ushort[] Multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

    private static readonly ushort[] Multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

    private readonly string _cnpjNumber;

    private string Masking => _cnpjNumber.OnlyNumbers().ToLong().Mask(DefaultMask);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cnpjNumber"></param>
    public Cnpj(string cnpjNumber)
    {
        _cnpjNumber = cnpjNumber;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
        var number = _cnpjNumber.OnlyNumbers();

        if (number.LengthNotEqualTo(DefaultLenght))
            return false;

        if (number.AllCharactersEquals())
            return false;

        var digit1 = Module11.Calculate(number.Substring(0, 12), Multiplier1);
        var digit2 = Module11.Calculate(number.Substring(0, 13), Multiplier2);

        if (!number[12].ToInt().Equals(digit1))
            return false;

        if (!number[13].ToInt().Equals(digit2))
            return false;

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string ToStringWithMask()
    {
        return IsValid()
            ? Masking
            : "Not a valid CNPJ";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string ToStringWithoutMask()
    {
        return IsValid()
            ? _cnpjNumber.OnlyNumbers()
            : "Not a valid CNPJ";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return IsValid()
            ? $"The {Masking} is a valid CNPJ"
            : $"The {_cnpjNumber} is a invalid CNPJ";
    }

    #region Static Methods

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static Cnpj GenerateRandom()
    {
        var cnpj = new Random().NextLong(11111111111, 999999999999);
        var digit1 = Module11.Calculate($"{cnpj}", Multiplier1);
        var digit2 = Module11.Calculate($"{cnpj}{digit1}", Multiplier2);

        return new Cnpj($"{cnpj}{digit1}{digit2}");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cnpjNumber"></param>
    /// <returns></returns>
    public static bool IsValid(string cnpjNumber)
    {
        return new Cnpj(cnpjNumber).IsValid();
    }

    #endregion
}