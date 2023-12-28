using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class DivideOpTests : _TupleTestsBase
    {

        [Fact]
        public void WhereATupleIsDivided_ThenTheOutputIsDivided()
        {
            //Given
            GivenATuple1(1, -2, 3, -4);

            //When
            WhenDividedOpIsCalled(2);

            //Then
            ThenResultShouldBeANewTuple(.5, -1, 1.5, -2);
        }

        public void WhenDividedOpIsCalled(double divisor)
        {
            _resultTuple = _tuple1 / divisor;
        }

    }
}
