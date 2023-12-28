using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.PlaneTests
{
    public class BoundsOfTests
    {
        private Shape _shape;
        private RayTracer.BoundingBox _boundingBoxResult;

        [Fact]
        public void WhereABoundingBox_ThenTheMinMaxIsReturned()
        {
            //Given
            GivenAShape(new Plane());

            //When
            WhenBoundsOfIsCalled(_shape);

            //Then
            ThenTheMinIs(Tuple.Point(-Constants.INFINITY, 0, -Constants.INFINITY));
            ThenTheMaxIs(Tuple.Point(Constants.INFINITY, 0, Constants.INFINITY));
        }

        private void ThenTheMaxIs(Tuple min)
        {
            _boundingBoxResult.Max.Should().Be(min);
        }

        private void ThenTheMinIs(Tuple max)
        {
            _boundingBoxResult.Min.Should().Be(max);
        }

        private void WhenBoundsOfIsCalled(Shape shape)
        {
            _boundingBoxResult = shape.BoundsOf();
        }

        private void GivenAShape(Shape shape)
        {
            _shape = shape;
        }
    }
}
