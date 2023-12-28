using FluentAssertions;

namespace RayTracer.Tests.BoundingBox
{
    public class _boundingBoxTestsBase
    {
        internal RayTracer.BoundingBox _boundingBoxResult;
        internal RayTracer.BoundingBox _boundingBox;
        internal Tuple _point1;
        internal Tuple _point2;

        internal void ThenTheMinIs(Tuple min)
        {
            _boundingBoxResult.Min.Should().Be(min);
        }

        internal void ThenTheMaxIs(Tuple max)
        {
            _boundingBoxResult.Max.Should().Be(max);
        }

        internal void GivenABoundingBox(RayTracer.BoundingBox boundingBox)
        {
            _boundingBox = boundingBox;
        }
    }
}
