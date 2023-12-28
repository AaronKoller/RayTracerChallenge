using Xunit;

namespace RayTracer.Tests.ColorTests
{
    public class AddOpTests : _ColorTestsBase
    {
        [Fact]
        public void WhereTwoColorsAdded_NewColorIsReturned()
        {
            //Given
            GivenColor1IsCreated(.9, .6, .75);
            GivenColor2IsCreated(.7, .1, .25);

            //When
            WhenColorOpAddIsCalled();


            //Then
            ThenResultColorIs(1.6, .7, 1);
        }


        protected void WhenColorOpAddIsCalled()
        {
            _resultColor = _color1 + _color2;
        }
    }
}
