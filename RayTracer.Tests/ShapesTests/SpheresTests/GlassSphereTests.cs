using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.SpheresTests
{
    public class GlassSphereTests
    {
        private GlassSphere _resultGlassSphere;

        [Fact]
        public void WhereAGlassSphereIsCreated_ItShouldHaveTheFollowingGlasslikeMaterial()
        {
            //When
            WhenAGlassSphereIsCreated(new GlassSphere());

            //Then
            ThenTheMaterialTransparencyIs(1.0);
            ThenTheMaterialRefractiveIndexIs(1.5);
        }

        private void ThenTheMaterialRefractiveIndexIs(double refractiveIndex)
        {
            _resultGlassSphere.Material.RefractiveIndex.Should().Be(refractiveIndex);
        }

        private void ThenTheMaterialTransparencyIs(double transparency)
        {
            _resultGlassSphere.Material.Transparency.Should().Be(transparency);
        }

        private void WhenAGlassSphereIsCreated(GlassSphere glassSphere)
        {
            _resultGlassSphere = glassSphere;
        }
    }
}
