using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class DotProductTests : _TupleTestsBase
    {

        [Fact]
        public void WhereDotProductIsCalled()
        {
            //Given
            GivenAVector1(1, 2, 3);
            GivenAVector2(2, 3, 4);

            //When
            WhenDotProductIsCalled();

            //Then
            ThenTheDotProductIsReturned(20);
        }
        private void WhenDotProductIsCalled()
        {
            _resultDouble = _vector1.Dot(_vector2);
        }

        private void ThenTheDotProductIsReturned(double dotProduct)
        {
            _resultDouble.Should().Be(dotProduct);
        }
    }
}
