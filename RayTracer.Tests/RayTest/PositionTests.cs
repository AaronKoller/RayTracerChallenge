using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.RayTest
{
    public class PositionTests : _RayTestsBase
    {
        private Tuple _point;

        [Theory]
        [InlineData(0, 2, 3, 4)]
        [InlineData(1, 3, 3, 4)]
        [InlineData(-1, 1, 3, 4)]
        [InlineData(2.5, 4.5, 3, 4)]
        public void WherePositionOpIsCalledAtTimeT_ThePositionIsComputed(double time, double x, double y, double z)
        {
            //Given
            GivenARay(new Ray(Tuple.Point(2, 3, 4), Tuple.Vector(1, 0, 0)));

            //When
            WhenPositionIsCalled(_ray, time);

            //Then
            ThenThePositionIsComputed(Tuple.Point(x, y, z));
        }



        private void WhenPositionIsCalled(Ray ray, double time)
        {
            _point = _ray.Position(time);
        }

        private void ThenThePositionIsComputed(Tuple point)
        {
            _point.Should().Be(point);
        }
    }
}
