using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.ConeTests
{
    public class BoundsOfTests
    {
        private Cone _cone;
        private RayTracer.BoundingBox _boundingBoxResult;

        [Fact]
        public void WhereABoundingBox_ThenTheMinMaxIsReturned()
        {
            //Given
            GivenAShape(new Cone());

            //When
            WhenBoundsOfIsCalled(_cone);

            //Then
            ThenTheMinIs(Tuple.Point(-Constants.INFINITY, -Constants.INFINITY, -Constants.INFINITY));
            ThenTheMaxIs(Tuple.Point(Constants.INFINITY, Constants.INFINITY, Constants.INFINITY));
        }

        [Fact]
        public void WhereABoundedCylinderHasABoundingBox_ThenTheMinMaxIsReturned()
        {
            //Given
            int minimum = -5;
            int maximum = 3;

            GivenAShape(new Cone());
            GivenTheConeHasAMinimum(minimum);
            GivenTheConeHasAMaximum(maximum);

            //When
            WhenBoundsOfIsCalled(_cone);

            //Then
            ThenTheMinIs(Tuple.Point(-5, -5, -5));
            ThenTheMaxIs(Tuple.Point(5, 3, 5));
        }

        private void GivenTheConeHasAMaximum(int maximum)
        {
            _cone.Maximum = maximum;
        }

        private void GivenTheConeHasAMinimum(int minimum)
        {
            _cone.Minimum = minimum;
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

        private void GivenAShape(Cone cone)
        {
            _cone = cone;
        }
    }
}
