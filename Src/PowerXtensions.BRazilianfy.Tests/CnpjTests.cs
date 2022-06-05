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
        }

        [Fact(DisplayName = "Test: Generate And Validate CNPJ")]
        public void GenerateAndValidateCnpjTests()
        {
            var _cpf = Cnpj.GenerateRandom();

            Assert.True(_cpf.IsValid());
        }
    }
}