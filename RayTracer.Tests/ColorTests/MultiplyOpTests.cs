using Xunit;

namespace RayTracer.Tests.ColorTests
{
    public class MultiplyOpTests : _ColorTestsBase
    {

        [Fact]
        public void WhereColorIsMultiplied_NewColorIsReturned()
        {
            //Given
            GivenColor1IsCreated(.2, .3, .4);

            //When
            WhenColorOpMultiplyIsCalled(2.0);

            //Then
            ThenResultColorIs(.4, .6, .8);
        }

        [Fact]
        public void WhereTwoColorsMultiplied_NewColorIsReturned()
        {
            //Given
            GivenColor1IsCreated(1, .2, .4);
            GivenColor2IsCreated(.9, 1, .1);

            //When
            WhenColorOpMultiplyIsCalled();

            //Then
            ThenResultColorIs(.9, .2, .04);
        }

        protected void WhenColorOpMultiplyIsCalled(double multiple)
        {
            _resultColor = _color1 * multiple;
        }

        protected void WhenColorOpMultiplyIsCalled()
        {
            _resultColor = _color1 * _color2;
        }
    }
}
