using System;
using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class ReflectTests : _TupleTestsBase
    {
        public static TheoryData<double, double, double, double, double, double, double, double, double> memberData()
        {
            double number = Math.Sqrt(2) / 2;
            return new TheoryData<double, double, double, double, double, double, double, double, double>
            {
                {1, -1, 0, 0, 1, 0, 1, 1, 0},
                {0, -1, 0, number, number, 0, 1, 0, 0},
            };
        }

        [Theory]
        [MemberData(nameof(memberData))]
        public void WhereAVector_ItCanBeReflected(
            double xV1, double yV1, double zV1,
            double xV2, double yV2, double zV2,
            double xV3, double yV3, double zV3
            )
        {
            //Given
            GivenAVector1(xV1, yV1, zV1);
            GivenAVector2(xV2, yV2, zV2);

            //When
            WhenReflectIsCalled(_vector1, _vector2);

            //Then
            ThenANewVectorResultIsCreated(xV3, yV3, zV3);
        }

        private void WhenReflectIsCalled(Tuple vector1, Tuple normal)
        {
            _resultVector = vector1.Reflect(normal);
        }
    }
}
