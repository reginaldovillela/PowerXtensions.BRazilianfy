namespace PowerXtensions.BRazilianfy.Contracts
{
    internal interface IDocument<T>
    {
        bool IsValid();

        string ToStringWithMask();

        string ToStringWithoutMask();
    }
}