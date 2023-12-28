using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using RayTracer.Patterns;
using Xunit;

namespace RayTracer.Tests.PatternsTests
{
    public class GradientPatternTests : _PatternTestsBase
    {
        [Theory]
        [InlineData(0.00,0,0,1.00,1.00,1.00)]
        [InlineData( .25,0,0, .75, .75, .75)]
        [InlineData( .50,0,0, .50, .50, .50)]
        [InlineData( .75,0,0, .25, .25, .25)]
        public void AGradientLinearlyInterpolatesBetweenColors_ThenTheColorIsReturned(double pX, double pY, double pZ, double red, double green, double blue)
        {
            //Given
            GivenAPattern(new GradientPattern(Color.White, Color.Black));

            //When
            WhenPatternAtIsCalled(Tuple.Point(pX,pY,pZ));

            //Then
            ThenTheColorIs(new Color(red, green, blue));
        }


    }
}
