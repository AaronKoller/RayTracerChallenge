using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.TriangleTests
{
    public class LocalNormalAtTests : _TriangleBaseTests
    {
        private Tuple _resultNormal;

        public static TheoryData<Tuple> FindingTheNormalOnATriangle1()
        {
            return new TheoryData<Tuple>
            {
                {Tuple.Point(0,.5,0)},
                {Tuple.Point(-.5,.75,0)},
                {Tuple.Point(.5,.25,0)},
            };
        }

        [Theory]
        [MemberData(nameof(FindingTheNormalOnATriangle1))]
        public void FindingTheNormalOnATriangle(Tuple point1)
        {
            //Given
            GivenATriangle(new Triangle(Tuple.Point(0,1,0), Tuple.Point(-1, 0, 0), Tuple.Point(1,0, 0)));

            //When
            WhenLocalNormalAtIsCalled(_triangle, point1);

            //Then
            ThenTheNormalIs(_triangle.Normal);
        }

        private void ThenTheNormalIs(Tuple triangleNormal)
        {
            _resultNormal.Should().Be(triangleNormal);
        }

        private void WhenLocalNormalAtIsCalled(Triangle triangle, Tuple point)
        {
            _resultNormal = triangle.LocalNormalAt(point);
        }
    }
}
