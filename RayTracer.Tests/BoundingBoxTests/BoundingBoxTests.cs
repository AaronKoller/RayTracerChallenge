using RayTracer.Tests.BoundingBox;
using Xunit;

namespace RayTracer.Tests.BoundingBoxTests
{
    public class BoundsTests : _boundingBoxTestsBase
    {
        private Tuple _min;
        private Tuple _max;


        [Fact]
        public void WhereCreatingAnEmptyBoundingBox_ThenThePropertiesAreReturned()
        {
            GivenAMin(Tuple.Point(Constants.INFINITY, Constants.INFINITY, Constants.INFINITY));
            GivenAMax(Tuple.Point(-Constants.INFINITY, -Constants.INFINITY, -Constants.INFINITY));

            //When
            WhenABoundingBoxIsCreated(new RayTracer.BoundingBox());

            //Then
            ThenTheMinIs(_min);
            ThenTheMaxIs(_max);
        }

        [Fact]
        public void WhereCreatingABoundingBoxWithVolume_ThenTheProperMinAndMaxAreReturned()
        {
            GivenAMin(Tuple.Point(-1, -2, -3));
            GivenAMax(Tuple.Point(3, 2, 1));
            //When
            WhenABoundingBoxIsCreated(boundingBox: new RayTracer.BoundingBox(_min, _max));

            //Then
            ThenTheMinIs(_min);
            ThenTheMaxIs(_max);
        }
        
        private void WhenABoundingBoxIsCreated(RayTracer.BoundingBox boundingBox)
        {
            _boundingBoxResult = boundingBox;
        }

        private void GivenAMax(Tuple max)
        {
            _max = max;
        }

        private void GivenAMin(Tuple min)
        {
            _min = min;
        }
    }
}
