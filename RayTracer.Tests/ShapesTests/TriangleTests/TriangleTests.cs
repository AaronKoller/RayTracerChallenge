using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.ShapesTests.TriangleTests
{
    public class TriangleTests : _TriangleBaseTests
    {
        private Tuple _point1;
        private Tuple _point2;
        private Tuple _point3;
        private Shapes.Triangle _triangleResult;

        [Fact]
        public void ConstructingATriangle()
        {
            //Given
            GivenApoint1(Tuple.Point(0, 1, 0));
            GivenApoint2(Tuple.Point(-1, 0, 0));
            GivenApoint3(Tuple.Point(1, 0, 0));

            //When
            WhenATriangleIsCreated(new Shapes.Triangle(_point1, _point2, _point3));

            //Then
            ThenAllPropertiesOnTheTriangleAre(_triangleResult);
        }

        private void ThenAllPropertiesOnTheTriangleAre(Shapes.Triangle triangleResult)
        {
            triangleResult.Point1.Should().Be(_point1);
            triangleResult.Point2.Should().Be(_point2);
            triangleResult.Point3.Should().Be(_point3);
            triangleResult.Edge1.Should().Be(Tuple.Vector(-1,-1,0));
            triangleResult.Edge2.Should().Be(Tuple.Vector(1,-1,0));
            triangleResult.Normal.Should().Be(Tuple.Vector(0,0,-1));
        }

        private void WhenATriangleIsCreated(Shapes.Triangle triangle)
        {
            _triangleResult = triangle;
        }

        private void GivenApoint3(Tuple point)
        {
            _point3 = point;
        }

        private void GivenApoint2(Tuple point)
        {
            _point2 = point;
        }

        private void GivenApoint1(Tuple point)
        {
            _point1 = point;
        }
    }
}
