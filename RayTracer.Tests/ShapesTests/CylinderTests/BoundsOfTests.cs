using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.CylinderTests
{
    public class BoundsOfTests
    {
        private Cylinder _cylinder;
        private RayTracer.BoundingBox _boundingBoxResult;

        [Fact]
        public void WhereABoundingBox_ThenTheMinMaxIsReturned()
        {
            //Given
            GivenAShape(new Cylinder());

            //When
            WhenBoundsOfIsCalled(_cylinder);

            //Then
            ThenTheMinIs(Tuple.Point(-1,-Constants.INFINITY,-1));
            ThenTheMaxIs(Tuple.Point(1,Constants.INFINITY,1));
        }

        [Fact]
        public void WhereABoundedCylinderHasABoundingBox_ThenTheMinMaxIsReturned()
        {
            //Given
            var minimum = -5;
            var maximum = 3;

            GivenAShape(new Cylinder());
            GivenTheCylinderHasAMinimum(minimum);
            GivenTheCylinderHasAMaximum(maximum);

            //When
            WhenBoundsOfIsCalled(_cylinder);

            //Then
            ThenTheMinIs(Tuple.Point(-1, minimum, -1));
            ThenTheMaxIs(Tuple.Point(1, maximum, 1));
        }

        private void GivenTheCylinderHasAMaximum(int max)
        {
            _cylinder.Maximum = max;
        }

        private void GivenTheCylinderHasAMinimum(int min)
        {
            _cylinder.Minimum = min;
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

        private void GivenAShape(Cylinder cylinder)
        {
            _cylinder = cylinder;
        }
    }
}
