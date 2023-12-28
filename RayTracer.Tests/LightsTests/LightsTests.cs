using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.LightsTests
{
    public class LightsTests
    {
        private Color _intensity;
        private Tuple _position;
        private Light _resultLight;

        [Fact]
        public void WhereALight_ItHasAPosition()
        {
            //Given
            GivenAnIntensity(new Color(1, 1, 1));
            GivenAPosition(Tuple.Point(0, 0, 0));

            //When
            WhenALightIsCreated(_position, _intensity);

            //Then
            ThenResultLightAsPosition(_position);
            ThenResultLightHasIntensity(_intensity);
        }

        private void WhenALightIsCreated(Tuple position, Color intensity)
        {
            _resultLight = new Light(position, intensity);
        }

        private void GivenAPosition(Tuple position)
        {
            _position = position;
        }

        private void ThenResultLightAsPosition(Tuple position)
        {
            _resultLight.Position.Should().Be(position);
        }

        private void ThenResultLightHasIntensity(Color intensity)
        {
            _resultLight.Intensity.Should().Be(intensity);
        }

        private void GivenAnIntensity(Color intensity)
        {
            _intensity = intensity;
        }
    }
}
