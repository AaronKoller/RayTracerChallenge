using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class NegateOpTests : _TupleTestsBase
    {
        [Fact]
        public void WhereAVectorIsNegated_TheOppositeVectorIsReturned()
        {
            //Given
            GivenATuple1(1, -2, 3, -4);

            //When
            WhenNegateOpIsCalled();

            //Then
            ThenResultShouldBeANewTuple(-1, 2, -3, 4);

        }

        public void WhenNegateOpIsCalled()
        {
            _resultTuple = -_tuple1;
        }

    }
}
