using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.IntersectionTests
{
    public class IntersectionTests
    {
        private Sphere _givenSphere;
        private double _givenT;
        private Intersection _resultIntersection;

        [Fact]
        public void AnIntersection_ShouldHaveAnIntersection()
        {
            //Given
            GivenASphere(new Sphere());
            GivenAnIntersectionT(3.5);

            //When
            WhenAnIntersectionIsCreated(_givenT, _givenSphere);

            //Then
            ThenItHasAnIntersection(3.5);
            ThenItHasAnObject(new Sphere());
        }

        private void WhenAnIntersectionIsCreated(double t, Sphere sphere)
        {
            _resultIntersection = new Intersection(t, sphere);
        }

        private void GivenAnIntersectionT(double t)
        {
            _givenT = t;
        }

        private void GivenASphere(Sphere sphere)
        {
            _givenSphere = sphere;
        }

        private void ThenItHasAnIntersection(double t)
        {
            _resultIntersection.T.Should().Be(t);
        }

        private void ThenItHasAnObject(Sphere sphere)
        {
            _resultIntersection.Object.Should().BeOfType<Sphere>();
        }
    }
}
