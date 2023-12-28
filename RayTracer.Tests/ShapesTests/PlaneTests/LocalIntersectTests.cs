using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.PlaneTests
{
    public class LocalIntersectAtTests : _PlaneTestsBase
    {
        private Ray _ray;
        private Intersections _resultIntersections;

        [Theory]
        [InlineData(0,10,0)]
        [InlineData(0,0,0)]
        public void WhereARayDoesNotIntersectWithPlane_NoIntersectionIsReturned(int pX, int pY, int pZ)
        {
            //Given
            GivenAPlane(new Plane());
            GivenARay(new Ray(Tuple.Point(pX, pY, pZ), Tuple.Vector(0, 0, 1)));

            //When
            WhenLocalIntersectIsCalled(_plane, _ray);

            //Then
            ThenTheIntersectionIsNull();
        }

        [Theory]
        [InlineData(0, 1, 0, 0,-1,0)]
        [InlineData(0,-1, 0, 0, 1,0)]
        public void WhereARayIntersectsWithPlane_OneIntersectionIsReturned(int pX, int pY, int pZ, int vX, int vY, int vZ)
        {
            //Given
            GivenAPlane(new Plane());
            GivenARay(new Ray(Tuple.Point(pX, pY, pZ), Tuple.Vector(vX, vY, vZ)));

            //When
            WhenLocalIntersectIsCalled(_plane, _ray);

            //Then
            TheIntersectionCountIt(1);
            TheIntersectionTIs(1);
            TheIntersectionObjectIs(_plane);
        }

        private void TheIntersectionObjectIs(Plane plane)
        {
            _resultIntersections[0].Object.Should().Be(plane);
        }

        private void TheIntersectionTIs(int i)
        {
            _resultIntersections[0].T.Should().Be(i);
        }

        private void TheIntersectionCountIt(int i)
        {
            _resultIntersections.Count.Should().Be(i);
        }

        private void GivenARay(Ray ray)
        {
            _ray = ray;
        }

        private void WhenLocalIntersectIsCalled(Plane plane, Ray ray)
        {
            _resultIntersections = plane.LocalIntersect(ray);
        }

        private void ThenTheIntersectionIsNull()
        {
            _resultIntersections.Should().BeNull();
        }
    }
}
