using Xunit;

namespace PowerXtensions.BRazilianfy.Tests
{
    public class CnpjTests
    {
        [Fact(DisplayName = "Test: Validate CNPJ")]
        public void ValidateCnpjTests()
        {
            var cnpj = new Cnpj("45.997.418/0001-53");

            Assert.True(cnpj.IsValid());
            Assert.True(Cnpj.IsValid("45.997.418/0001-53"));
            //Assert.False(_cpf.Validate());
        }

        // [Fact(DisplayName = "Test: Generate And Validate CPF")]
        // public void GenerateAndValidateCpfTests()
        // {
        //     var _cpf = Cpf.GenerateRandom();

        //     Assert.True(_cpf.IsValid());
        //     //Assert.False(_cpf.Validate());
        // }
    }
}