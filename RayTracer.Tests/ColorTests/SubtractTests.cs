using Xunit;

namespace RayTracer.Tests.ColorTests
{
    public class SubtractTests : _ColorTestsBase
    {
        [Fact]
        public void WhereTwoColorsSubtracted_NewColorIsReturned()
        {
            //Given
            GivenColor1IsCreated(.9, .6, .75);
            GivenColor2IsCreated(.7, .1, .25);

            //When
            WhenColorOpSubtractIsCalled();

            //Then
            ThenResultColorIs(.2, .5, .5);
        }

        protected void WhenColorOpSubtractIsCalled()
        {
            _resultColor = _color1 - _color2;
        }

    }
}
