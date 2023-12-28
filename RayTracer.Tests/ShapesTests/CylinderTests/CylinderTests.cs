using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.CylinderTests
{
    public class CylinderTests
    {
        private Cylinder _cylinderResult;

        [Fact]
        public void WhereANewCylinderItHasTheFollowingProperties()
        {
            //When
            WhenANewCylinderIsCreated(new Cylinder());

            //Then
            ThenTheCylinderMinimumIs(_cylinderResult, double.MinValue);
            ThenTheCylinderMaximumIs(_cylinderResult, double.MaxValue);
            ThenTheCylinderIsClosed();
        }

        private void ThenTheCylinderIsClosed()
        {
            _cylinderResult.Closed.Should().BeFalse();
        }

        private void ThenTheCylinderMinimumIs(Cylinder cylinder, double value)
        {
            _cylinderResult.Minimum.Should().Be(value);
        }

        private void ThenTheCylinderMaximumIs(Cylinder cylinder, double value)
        {
            _cylinderResult.Maximum.Should().Be(value);
        }

        private void WhenANewCylinderIsCreated(Cylinder cylinder)
        {
            _cylinderResult = cylinder;
        }
    }
}
