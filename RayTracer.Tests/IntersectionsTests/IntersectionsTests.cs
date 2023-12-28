using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.IntersectionsTests
{
    public class IntersectionsTests : _IntersectionsTestsBase
    {

        private Intersections _resultIntersections;

        [Fact]
        public void WhereMultipleIntersectionsGiven_IntersectionsAreReturned()
        {
            //Given
            GivenAShape(new Sphere());
            GivenAnIntersection0(new Intersection(1, _shape));
            GivenAnIntersection1(new Intersection(2, _shape));

            //When
            WhenIntersectionsIsCreated(_givenIntersection0, _givenIntersection1);

            //Then
            ThenIntersectCountIs(2);
            ThenTheX0IntersectPositionIs(1);
            ThenTheX1IntersectPositionIs(2);
        }


        private void WhenIntersectionsIsCreated(Intersection givenIntersection1, Intersection givenIntersection2)
        {
            _resultIntersections = new Intersections
            {
                [0] = givenIntersection1,
                [1] = givenIntersection2
            };
        }

        private void ThenTheX1IntersectPositionIs(int i)
        {
            _resultIntersections[1].T.Should().Be(i);
        }

        private void ThenTheX0IntersectPositionIs(int i)
        {
            _resultIntersections[0].T.Should().Be(i);
        }

        private void ThenIntersectCountIs(int i)
        {
            _resultIntersections.Count.Should().Be(i);
        }
    }
}
