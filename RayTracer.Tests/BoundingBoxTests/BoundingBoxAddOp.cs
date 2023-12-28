using FluentAssertions;
using RayTracer.Tests.BoundingBox;
using Xunit;

namespace RayTracer.Tests.BoundingBoxTests
{
    public class BoundingBoxAddOp : _boundingBoxTestsBase
    {
        private RayTracer.BoundingBox _boundingBox1;
        private RayTracer.BoundingBox _boundingBox2;

        [Fact]
        public void WhereAddingOneBoundingBoxToAnother_ThenThenANewBoundingBoxIsFormed()
        {
            //Given
            GivenABoundingBox1(new RayTracer.BoundingBox(Tuple.Point(-5,-2, 0),Tuple.Point(7, 4, 4)));
            GivenABoundingBox2(new RayTracer.BoundingBox(Tuple.Point(8,-7,-2),Tuple.Point(14,2,8)));

            //When
            WhenAddOpIsCalled(_boundingBox1, _boundingBox2);

            //Then
            ThenResultBoxMin(Tuple.Point(-5, -7, -2));
            ThenResultBoxMax(Tuple.Point(14,4,8));
        }

        private void ThenResultBoxMax(Tuple point)
        {
            _boundingBoxResult.Max.Should().Be(point);
        }

        private void ThenResultBoxMin(Tuple point)
        {
            _boundingBoxResult.Min.Should().Be(point);
        }

        private void WhenAddOpIsCalled(RayTracer.BoundingBox boundingBox1, RayTracer.BoundingBox boundingBox2)
        {
            _boundingBoxResult = boundingBox1 + boundingBox2;
        }

        private void GivenABoundingBox2(RayTracer.BoundingBox boundingBox)
        {
            _boundingBox2 = boundingBox;
        }

        private void GivenABoundingBox1(RayTracer.BoundingBox boundingBox)
        {
            _boundingBox1 = boundingBox;
        }
    }
}
