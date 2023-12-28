using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.ColorTests
{
    public class ColorTests : _ColorTestsBase
    {
        [Fact]
        public void WhereColorSet_ColorPropertiesReturned()
        {
            //Given
            GivenColor1IsCreated(-.5, .4, 1.7);

            //Then
            ThenColor1RedPropertyIs(-.5);
            ThenColor1GreenPropertyIs(.4);
            ThenColor1BluePropertyIs(1.7);
        }

        protected void ThenColor1RedPropertyIs(double red)
        {
            _color1.RedChannel.Should().Be(red);
        }

        protected void ThenColor1GreenPropertyIs(double Green)
        {
            _color1.GreenChannel.Should().Be(Green);
        }

        protected void ThenColor1BluePropertyIs(double Blue)
        {
            _color1.BlueChannel.Should().Be(Blue);
        }
    }
}
