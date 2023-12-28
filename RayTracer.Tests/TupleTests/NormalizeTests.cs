using System;
using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class NormalizeTests : _TupleTestsBase
    {

        [Theory]
        [InlineData(4, 0, 0)]
        [InlineData(1, 2, 3)]
        public void WhereNormalizedIsCalled(double x, double y, double z)
        {
            //Given
            GivenAVector1(x, y, z);

            //When
            WhenNormalizedIsCalled();

            //Then
            ThenTheNormalizationIsReturned(Normalize(x, _vector1), Normalize(y, _vector1), Normalize(z, _vector1));
        }

        public double Normalize(double x, Tuple vector)
        {
            return Math.Abs(x) < Constants.EPSILON ? 0 : x / Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2) + Math.Pow(vector.Z, 2));
        }

        public void WhenNormalizedIsCalled()
        {
            _resultVector = _vector1.Normalize();
        }

        public void ThenTheNormalizationIsReturned(double x, double y, double z)
        {
            _resultVector.Should().Be(Tuple.Vector(x, y, z));
        }
    }
}
