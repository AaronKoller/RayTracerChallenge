using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class MultiplyOp : _TupleTestsBase
    {
        [Fact]
        public void WhereATupleIsMultipliedByAScalar_ThenTheOutputTupleIsMultiplied()
        {
            //Given
            GivenATuple1(1, -2, 3, -4);

            //When
            WhenMultipliedOpIsCalled(3.5);

            //Then
            ThenResultShouldBeANewTuple(3.5, -7, 10.5, -14);
        }

        [Fact]
        public void WhereATupleIsMultipliedByAFraction_ThenTheOutputTupleIsMultiplied()
        {
            //Given
            GivenATuple1(1, -2, 3, -4);

            //When
            WhenMultipliedOpIsCalled(0.5);

            //Then
            ThenResultShouldBeANewTuple(.5, -1, 1.5, -2);
        }

        public void WhenMultipliedOpIsCalled(double d)
        {
            _resultTuple = _tuple1 * d;
        }
    }
}
