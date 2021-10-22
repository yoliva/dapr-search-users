using Oiga.Common.Pagination;
using Xunit;

namespace Oiga.Common.UnitTests
{
    public class HelpersTests
    {
        [Fact]
        public void Encode_WithValidValues_EncodeProperlyTheToken()
        {
            var token = Helpers.Encode(10, 4);

            var contToken = Helpers.Decode(token);
            Assert.Equal(10, contToken.Limit);
            Assert.Equal(4, contToken.SkipCount);
        }

        [Fact]
        public void Decode_WithValidValues_EncodeProperlyTheToken()
        {
            var contToken = Helpers.Decode("eyJTa2lwQ291bnQiOjQsIkxpbWl0IjoxMH0=");

            Assert.Equal(10, contToken.Limit);
            Assert.Equal(4, contToken.SkipCount);
        }
    }
}
