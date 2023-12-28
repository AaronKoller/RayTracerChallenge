using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.CylinderTests
{
    public class NormalAtTests : _CylinderBaseTests
    {
        private Tuple _normal;

        public static TheoryData<Tuple, Tuple> LocalIntersectData1()
        {
            return new TheoryData<Tuple, Tuple>
            {
                {Tuple.Point( 1, 0, 0), Tuple.Vector( 1,0, 0)},
                {Tuple.Point( 0, 5,-1), Tuple.Vector( 0,0,-1)},
                {Tuple.Point( 0,-2, 1), Tuple.Vector( 0,0, 1)},
                {Tuple.Point(-1, 1, 0), Tuple.Vector(-1,0, 0)},
            };
        }

        [Theory]
        [MemberData(nameof(LocalIntersectData1))]
        public void WhereNormalVectorOnACylinder(Tuple point, Tuple normal)
        {
            //Given
            GivenAShape(new Cylinder());

            //When
            WhenNormalAtIsCalled(_shape, point);

            //Then
            ThenTheNormalIs(normal);
        }

        public static TheoryData<Tuple, Tuple> LocalIntersectData2()
        {
            return new TheoryData<Tuple, Tuple>
            {
                {Tuple.Point( 0,1, 0), Tuple.Vector(0,-1,0)},
                {Tuple.Point(.5,1, 0), Tuple.Vector(0,-1,0)},
                {Tuple.Point( 0,1,.5), Tuple.Vector(0,-1,0)},
                {Tuple.Point( 0,2, 0), Tuple.Vector(0, 1,0)},
                {Tuple.Point(.5,2, 0), Tuple.Vector(0, 1,0)},
                {Tuple.Point( 0,2,.5), Tuple.Vector(0, 1,0)},
            };
        }

        [Theory]
        [MemberData(nameof(LocalIntersectData2))]
        public void WhereNormalVectorOnACylinderWithEndCaps(Tuple point, Tuple normal)
        {
            //Given
            GivenAShape(new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            });

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
