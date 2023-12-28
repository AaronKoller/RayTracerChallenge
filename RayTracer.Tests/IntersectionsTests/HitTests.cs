using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.IntersectionsTests
{
    public class HitTests : _IntersectionsTestsBase
    {
        private Intersections _givenIntersections;
        private Intersection _resultIntersections;

        [Theory]
        [InlineData(1,2,1)]
        [InlineData(-1,1,1)]
        [InlineData(-1,1,1)]
        public void WhereMultipleHitsAreFound_OnlyLowestNonNegativeHitIsReturned(double t0, double t1, double expectedT)
        {
            //Given
            GivenAShape(new Sphere());
            GivenAnIntersection0(new Intersection(t0, _shape));
            GivenAnIntersection1(new Intersection(t1, _shape));
            Given2Intersections(_givenIntersection0, _givenIntersection1);

            //When
            WhenHitIsCalled(_givenIntersections);

            //Then
            ThenOnlyThisIntersectionIsReturned(expectedT);
        }

        [Theory]
        [InlineData(-2, -1)]
        public void WhereMultipleNegativeHitsAreFound_NoHitIsReturned(double t0, double t1)
        {
            //Given
            GivenAShape(new Sphere());
            GivenAnIntersection0(new Intersection(t0, _shape));
            GivenAnIntersection1(new Intersection(t1, _shape));
            Given2Intersections(_givenIntersection0, _givenIntersection1);

            //When
            WhenHitIsCalled(_givenIntersections);

            //Then
            ThenNoIntersectionsAreReturned();
        }

        [Fact]
        public void WhereMoreThanTwoIntersectionsAreDefined_OnlyLowestNonNegativeHitIsReturned()
        {
            //Given
            GivenAShape(new Sphere());
            GivenAnIntersection0(new Intersection( 5, _shape));
            GivenAnIntersection1(new Intersection( 7, _shape));
            GivenAnIntersection2(new Intersection(-3, _shape));
            GivenAnIntersection3(new Intersection( 2, _shape));
            Given4Intersections(_givenIntersection0, _givenIntersection1, _givenIntersection2, _givenIntersection3);

            //When
            WhenHitIsCalled(_givenIntersections);

            //Then
            ThenOnlyThisIntersectionIsReturned(2);
        }


        private void Given2Intersections(Intersection givenIntersection0, Intersection givenIntersection1)
        {
            _givenIntersections = new Intersections
            {
                [0] = givenIntersection0,
                [1] = givenIntersection1
            };
        }

        private void Given4Intersections(Intersection givenIntersection0, Intersection givenIntersection1, Intersection givenIntersection2, Intersection givenIntersection3)
        {
            _givenIntersections = new Intersections
            {
                [0] = givenIntersection0,
                [1] = givenIntersection1,
                [2] = givenIntersection2,
                [3] = givenIntersection3,
            };
        }


        private void WhenHitIsCalled(Intersections givenIntersections)
        {
            _resultIntersections = _givenIntersections.Hit();
        }

        private void ThenNoIntersectionsAreReturned()
        {
            _resultIntersections.Should().BeNull();
        }

        private void ThenOnlyThisIntersectionIsReturned(double intersection)
        {
            _resultIntersections.T.Should().Be(intersection);
        }
    }
}
