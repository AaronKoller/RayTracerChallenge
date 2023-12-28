using FluentAssertions;
using System;
using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class MagnitudeTests : _TupleTestsBase
    {


        [Theory]
        [InlineData(1, 0, 0, 1)]
        [InlineData(0, 1, 0, 1)]
        [InlineData(0, 0, 1, 1)]
        [InlineData(1, 2, 3, 14)]
        [InlineData(-1, -2, -3, 14)]
        public void WhereMagnitudeIsCalled(double x, double y, double z, double magnitude)
        {
            //Given
            GivenAVector1(x, y, z);

            //When
            WhenMagnitudeIsCalled();

            //Then
            ThenTheMagnitudeIsReturned(Math.Sqrt(magnitude));

        }

        public void WhenMagnitudeIsCalled()
        {
            _magnitudeResult = _vector1.Magnitude();
        }

        public void ThenTheMagnitudeIsReturned(double magnitude)
        {
            _magnitudeResult.Should().Be(magnitude);
        }

    }
}
