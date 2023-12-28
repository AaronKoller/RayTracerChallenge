using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class AddOpTests : _TupleTestsBase
    {
        [Fact]
        public void WhereTwoTuplesAreAdded_ThenANewSummedTupleIsCreated()
        {
            //Given
            GivenATuple1(3, -2, 5, 1);
            GivenATuple2(-2, 3, 1, 0);

            //When
            WhenAddOpIsCalled();

            //Then
            ThenResultShouldBeANewTuple(1, 1, 6, 1);
        }

        [Fact]
        public void WhereTwoPointsAreAdded_InvalidOperationShouldBeReturned()
        {
            //Given
            GivenAPoint1(3, -2, 5);
            GivenAPoint2(-2, 3, 1);

            //When
            WhenAddWithExceptionIsCalled();

            //Then
            ThenInvalidOperationWithIsReturnedWithMessage("Two points cannot be added.");
        }

        public void WhenAddOpIsCalled()
        {
            _resultTuple = _tuple1 + _tuple2;
        }

        public void WhenAddWithExceptionIsCalled()
        {
            _exceptionResult = () => { var test = _point1 + _point2; };
        }

    }
}
