using Xunit;

namespace RayTracer.Tests.BoundingBox
{
    public class ResizeTests : _boundingBoxTestsBase
    {

        [Fact]
        public void WhereAddingPointsToAnEmptyBoundingBox_TheTheResiezedMinMaxAreReturned()
        {
            //Given
            GivenABoundingBox(new RayTracer.BoundingBox());
            GivenAPoint1(Tuple.Point(-5, 2, 0));
            GivenAPoint2(Tuple.Point(7, 0, -3));
            //When
            WhenResizePointIsCalledForTwoPoints(_point1, _point2);

            //Then
            ThenTheMinIs(Tuple.Point(-5, 0, -3));
            ThenTheMaxIs(Tuple.Point(7, 2, 0));
        }

        private void WhenResizePointIsCalledForTwoPoints(Tuple point1, Tuple point2)
        {
            _boundingBox.Resize(point1);
            _boundingBox.Resize(point2);
            _boundingBoxResult = _boundingBox;
        }

        private void GivenAPoint2(Tuple point)
        {
            _point2 = point;
        }

        private void GivenAPoint1(Tuple point)
        {
            _point1 = point;
        }


    }
}
