using BackendTest.Domain.Queries.IntelligentBillboard;
using FluentAssertions;
using Xunit;

namespace BackendTest.Domain.Tests.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardValidatorShould
    {
        [Theory]
        [InlineData(-1, 1, 1, true, false)]
        [InlineData(0, 1, 1, true, false)]
        [InlineData(30, 1, 1, true, true)]
        [InlineData(365, 1, 1, true, true)]
        [InlineData(366, 1, 1, true, false)]
        [InlineData(30, -1, 1, true, false)]
        [InlineData(30, 1, -1, true, false)]
        [InlineData(30, 0, 0, true, false)]
        [InlineData(30, 1, 0, true, true)]
        [InlineData(30, 0, 1, true, true)]
        public void ValidateTheRequest(int periodOfTimeInDays, int bigScreens, int smallScreens, bool city, bool isValid)
        {
            var validator = new GetIntelligentBillboardValidator();
            var request = new GetIntelligentBillboardRequest(periodOfTimeInDays, bigScreens, smallScreens, city);

            var result = validator.Validate(request);

            result.IsValid.Should().Be(isValid);
        }
    }
}