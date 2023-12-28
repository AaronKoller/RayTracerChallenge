using System;
using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.ConeTests
{
    public class NormalAtTests : _ConeBaseTests
    {
        private Tuple _normal;

        public static TheoryData<Tuple, Tuple> LocalIntersectData1()
        {
            return new TheoryData<Tuple, Tuple>
            {
                {Tuple.Point(0,0,0), Tuple.Vector(0,0,0)},
                {Tuple.Point(1,1,1), Tuple.Vector(1,- Math.Sqrt(2),1)},
                {Tuple.Point(-1,-1,0), Tuple.Vector(-1,1,0)},
            };
        }

        [Theory]
        [MemberData(nameof(LocalIntersectData1))]
        public void WhereComputingTheNormalVectorOnACone(Tuple point, Tuple normal)
        {
            //Given
            GivenAShape(new Cone());

            //When
            WhenNormalAtIsCalled(_shape, point);

            //Then
            ThenTheNormalIs(normal);
        }

        private void ThenTheNormalIs(Tuple normal)
        {
            _normal.Should().Be(normal);
        }

        private void WhenNormalAtIsCalled(Shape shape, Tuple point)
        {
            _normal = shape.LocalNormalAt(point);
        }
    }
}
