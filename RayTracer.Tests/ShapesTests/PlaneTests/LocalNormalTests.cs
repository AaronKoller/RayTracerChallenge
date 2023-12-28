using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.PlaneTests
{
    public class PlaneTests : _PlaneTestsBase
    {
        private Tuple _resultVector;

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(-10, 0, -10)]
        [InlineData(-5, 0, 150)]
        public void WhereThePlaneNormalIsCalled_ItIsConstantEverywhere(int pX, int pY, int pZ)
        {
            //Given
            GivenAPlane(new Plane());

            //When
            WhenLocalNormalIsAtIsCalled(Tuple.Point(pX, pY, pZ));

            //Then
            ThenTheVectorIs(Tuple.Vector(0, 1, 0));
        }

        private void WhenLocalNormalIsAtIsCalled(Tuple point)
        {
            _resultVector = _plane.LocalNormalAt(point);
        }

        private void ThenTheVectorIs(Tuple vector)
        {
            _resultVector.Should().Be(vector);
        }
    }
}
