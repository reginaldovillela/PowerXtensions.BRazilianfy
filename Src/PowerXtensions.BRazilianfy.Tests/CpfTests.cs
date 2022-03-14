using Xunit;

namespace PowerXtensions.BRazilianfy.Tests
{
    public class CpfTests
    {
        [Fact(DisplayName = "Test: Validate CPF")]
        public void ValidateCpfTests()
        {
            var _cpf = new Cpf("331.519.568-40");

            Assert.True(_cpf.IsValid());
            //Assert.False(_cpf.Validate());
        }

        [Fact(DisplayName = "Test: Generate And Validate CPF")]
        public void GenerateAndValidateCpfTests()
        {
            var _cpf = Cpf.GenerateRandom();

            Assert.True(_cpf.IsValid());
            //Assert.False(_cpf.Validate());
        }
    }
}