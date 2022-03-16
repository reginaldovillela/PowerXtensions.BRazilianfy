using Xunit;

namespace PowerXtensions.BRazilianfy.Tests
{
    public class PisTests
    {
        private const string PIS_VALID = "112.85163.25-1";
        private const string PIS_INVALID = "112.85163.25-9";

        [Fact(DisplayName = "Test: Validate PIS")]
        public void ValidatePISTests()
        {
            var pisValid = new Pis(PIS_VALID);
            var pisInvalid = new Pis(PIS_INVALID);

            Assert.True(pisValid.IsValid());
            Assert.False(pisInvalid.IsValid());
        }
    }
}