using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class CrossProductTests : _TupleTestsBase
    {

        [Theory]
        [InlineData(1, 2, 3, 2, 3, 4, -1, 2, -1)]
        [InlineData(2, 3, 4, 1, 2, 3, 1, -2, 1)]
        public void WhereCrossProductIsCalled(
            double vector1X, double vector1Y, double vector1Z,
            double vector2X, double vector2Y, double vector2Z,
            double vector3X, double vector3Y, double vector3Z)
        {
            //Given
            GivenAVector1(vector1X, vector1Y, vector1Z);
            GivenAVector2(vector2X, vector2Y, vector2Z);

            //When
            WhenCrossProductIsCalled();

            //Then
            ThenANewVectorResultIsCreated(vector3X, vector3Y, vector3Z);
        }

        public void WhenCrossProductIsCalled()
        {
            _resultVector = _vector1.Cross(_vector2);
        }

    }
}
