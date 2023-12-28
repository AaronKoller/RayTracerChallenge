using FluentAssertions;
using RayTracer.Shapes;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.ShapesTests.SpheresTests
{
    public class IntersectTests : _SphereTestsBase
    {
        private Ray _ray;
        private Intersections _resultIntersections;

        [Theory]
        [InlineData(0, 0,-5, 4.0, 6.0, 2)]
        [InlineData(0, 1,-5, 5.0, 5.0, 2)]
        [InlineData(0, 0, 0,-1.0, 1.0, 2)]
        [InlineData(0, 0, 5,-6.0,-4.0, 2)]
        public void WhereARayIsOutsideAndIntersectsUnitSphere_TheRayShouldIntersectAtTwoPoints(double x, double y, double z, double x0, double x1, int intersections)
        {
            //Given
            GivenARay(new Ray(Tuple.Point(x, y, z), Tuple.Vector(0, 0, 1)));
            //GivenASphere(); //Intersection method assumes to be at Point(0,0,0) and radius 1
            GivenASphere(new Sphere());

            //When
            WhenIntersectIsCalled(_ray, _sphere);

            //Then
            ThenIntersectCountIs(intersections);
            ThenTheX0IntersectPositionIs(x0);
            ThenTheX1IntersectPositionIs(x1);
            ThenTheX0TypeIs(new Sphere());
            ThenTheX1TypeIs(new Sphere());
        }

        [Theory]
        [InlineData(0, 2, -5, 0)]
        public void WhereARayIsOutsideAndDoesNotIntersectsUnitSphere_TheRayShouldNotIntersect(double x, double y, double z, int intersections)
        {
            //Given
            GivenARay(new Ray(Tuple.Point(x, y, z), Tuple.Vector(0, 0, 1)));
            GivenASphere(new Sphere());

            //When
            WhenIntersectIsCalled(_ray, _sphere);

            //Then
            ThenIntersectCountIs(intersections);
        }

        [Fact]
        public void WhereARayIntersectsAScaledSphere_TheIntersectsAreReturned()
        {
            //Given
            GivenARay(new Ray(Tuple.Point(0,0,-5), Tuple.Vector(0, 0, 1)));
            GivenASphere(new Sphere());
            GivenAScaleTransformation(2,2,2);
            GivenTheTransformationIsSetOnASphere(_transformMatrix);

            //When
            WhenIntersectIsCalled(_ray, _sphere);

            //Then
            ThenIntersectCountIs(2);
            ThenTheX0IntersectPositionIs(3);
            ThenTheX1IntersectPositionIs(7);
        }

        [Fact]
        public void WhereARayIntersectsATranslatedSphere_TheIntersectsAreReturned()
        {
            //Given
            GivenARay(new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1)));
            GivenASphere(new Sphere());
            GivenATranslationTransformation(5,0,0);
            GivenTheTransformationIsSetOnASphere(_transformMatrix);

            //When
            WhenIntersectIsCalled(_ray, _sphere);

            //Then
            TheThereAreNoInterSections();
        }

        private void TheThereAreNoInterSections()
        {
            _resultIntersections.Count.Should().Be(0);
        }

        private void GivenAScaleTransformation(double x, double y, double z)
        {
            _transformMatrix = Matrix.Transform.Scale(x, y, z);
        }


        private void GivenARay(Ray ray)
        {
            _ray = ray;
        }

        private void WhenIntersectIsCalled(Ray ray, Sphere sphere)
        {
            _resultIntersections = sphere.Intersect(ray);
        }

        private void ThenIntersectCountIs(int intersections)
        {
            _resultIntersections.Count.Should().Be(intersections);
        }

        private void ThenTheX0IntersectPositionIs(double x0)
        {
            _resultIntersections[0].T.Should().Be(x0);
        }

        private void ThenTheX1IntersectPositionIs(double x1)
        {
            _resultIntersections[1].T.Should().Be(x1);
        }

        private void ThenTheX0TypeIs(Sphere sphere)
        {
            _resultIntersections[0].Object.Should().BeOfType<Sphere>();
        }

        private void ThenTheX1TypeIs(Sphere sphere)
        {
            _resultIntersections[1].Object.Should().BeOfType<Sphere>();
        }
    }
}
