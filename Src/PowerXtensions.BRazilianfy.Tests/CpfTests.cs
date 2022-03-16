using Xunit;

namespace PowerXtensions.BRazilianfy.Tests
{
    public class CpfTests
    {
        private const string CPF_VALID = "395.478.930-25";
        private const string CPF_INVALID = "395.478.930-36";

        [Fact(DisplayName = "Test: Validate CPF")]
        public void ValidateCpfTests()
        {
            var cpfValid = new Cpf(CPF_VALID);
            var cpfInvalid = new Cpf(CPF_INVALID);

            Assert.True(cpfValid.IsValid());
            Assert.False(cpfInvalid.IsValid());
        }

        [Fact(DisplayName = "Test: Validate CPF (Static)")]
        public void ValidateStaticCpfTests()
        {
            Assert.True(Cpf.IsValid(CPF_VALID));
            Assert.False(Cpf.IsValid(CPF_INVALID));
        }

        [Fact(DisplayName = "Test: Generate And Validate CPF")]
        public void GenerateAndValidateCpfTests()
        {
            var cpf = Cpf.GenerateRandom();

            Assert.True(cpf.IsValid());
        }
    }
}