using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.TestShapeTests
{
    public class BoundsOfTests
    {
        private Shape _shape;
        private RayTracer.BoundingBox _boundingBoxResult;

        [Fact]
        public void WhereABoundingBox_ThenTheMinMaxIsReturned()
        {
            //Given
            GivenAShape(new TestShape());

            //When
            WhenBoundsOfIsCalled(_shape);

            //Then
            ThenTheMinIs(Tuple.Point(-1,-1,-1));
            ThenTheMaxIs(Tuple.Point(1,1,1));
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
