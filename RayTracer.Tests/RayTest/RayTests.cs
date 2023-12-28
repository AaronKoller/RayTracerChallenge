using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.RayTest
{
    public class RayTests : _RayTestsBase
    {
        private Tuple _origin;
        private Tuple _direction;

        [Fact]
        public void GivenARay_ThenTheRayOriginIsReturned()
        {
            //Given
            GivenAnOrigin(Tuple.Point(1, 2, 3));
            GivenADirection(Tuple.Vector(4, 5, 6));

            //When
            WhenARayIsCreated(_origin, _direction);

            //Then
            ThenTheOriginIs(_origin);
        }

        [Fact]
        public void GivenARay_ThenTheRayDirectionIsReturned()
        {
            //Given
            GivenAnOrigin(Tuple.Point(1, 2, 3));
            GivenADirection(Tuple.Vector(4, 5, 6));

            //When
            WhenARayIsCreated(_origin, _direction);

            //Then
            ThenTheDirectionIs(_direction);
        }



        private void WhenARayIsCreated(Tuple origin, Tuple direction)
        {
            _resultRay = new Ray(origin, direction);
        }

        private void GivenADirection(Tuple vector)
        {
            _direction = vector;
        }

        private void GivenAnOrigin(Tuple point)
        {
            _origin = point;
        }


    }
}
