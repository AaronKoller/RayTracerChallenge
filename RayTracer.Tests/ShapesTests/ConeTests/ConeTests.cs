using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.ConeTests
{
    public class ConeTests
    {
        private Cone _coneResult;

        [Fact]
        public void WhereANewCylinderItHasTheFollowingProperties()
        {
            //When
            WhenANewConeIsCreated(new Cone());

            //Then
            ThenTheConeMinimumIs(_coneResult, double.MinValue);
            ThenTheConeMaximumIs(_coneResult, double.MaxValue);
            ThenTheConeIsClosed();
        }

        private void ThenTheConeIsClosed()
        {
            _coneResult.Closed.Should().BeFalse();
        }

        private void ThenTheConeMinimumIs(Cone cone, double value)
        {
            _coneResult.Minimum.Should().Be(value);
        }

        private void ThenTheConeMaximumIs(Cone cone, double value)
        {
            _coneResult.Maximum.Should().Be(value);
        }

        private void WhenANewConeIsCreated(Cone cone)
        {
            _coneResult = cone;
        }
    }
}
