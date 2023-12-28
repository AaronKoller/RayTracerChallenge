using System.Collections.Generic;
using System.Text;
using RayTracer.Patterns;
using Xunit;

namespace RayTracer.Tests.PatternsTests
{
    public class CheckersPatternsTests : _PatternTestsBase
    {

        public static TheoryData<double, double, double, Color> memberData()
        {
            return new TheoryData<double, double, double, Color>
            {
                {   0,   0,   0, Color.White},
                { .99,   0,   0, Color.White},
                {1.01,   0,   0, Color.Black},

                {   0,   0,   0, Color.White},
                {   0, .99,   0, Color.White},
                {   0,1.01,   0, Color.Black},

                {   0,   0,   0, Color.White},
                {   0,   0, .99, Color.White},
                {   0,   0,1.01, Color.Black},
            };
        }

        [Theory]
        [MemberData(nameof(memberData))]
        public void CheckersShouldRepeat(double pX, double pY, double pZ, Color color)
        {
            //Given
            GivenAPattern(new CheckersPatterns(Color.White, Color.Black));

            //When
            WhenPatternAtIsCalled(Tuple.Point(pX, pY, pZ));

            //Then
            ThenTheColorIs(color);
        }
    }
}
