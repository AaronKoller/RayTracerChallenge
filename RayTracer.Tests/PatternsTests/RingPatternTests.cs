using RayTracer.Patterns;
using Xunit;

namespace RayTracer.Tests.PatternsTests
{
    public class RingPatternTests : _PatternTestsBase
    {

        public static TheoryData<double, double, double, Color> memberData()
        {
            return new TheoryData<double, double, double, Color>
            {
                {0, 0, 0, Color.White},
                {1, 0, 0, Color.Black},
                {0, 0, 1, Color.Black},
                {.708, 0, .708, Color.Black},
            };
        }

        [Theory]
        [MemberData(nameof(memberData))]
        public void ARingShouldExtendInBothXAndZ(double pX, double pY, double pZ, Color color)
        {
            //Given
            GivenAPattern(new RingPattern(Color.White, Color.Black));

            //When
            WhenPatternAtIsCalled(Tuple.Point(pX, pY, pZ));

            //Then
            ThenTheColorIs(color);
        }
    }
}
