using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.CubeTests
{
    public class LocalNormalAtTests : _CubeBaseTests
    {
        private Tuple _point;
        private Tuple _normal;

        public static TheoryData<Tuple, Tuple> LocalNormalData1()
        {
            return new TheoryData<Tuple, Tuple>
            {
                {Tuple.Point( 1.0,  .5, -.8), Tuple.Vector( 1, 0, 0)},
                {Tuple.Point(-1.0, -.2,  .9), Tuple.Vector(-1, 0, 0)},
                {Tuple.Point( -.4, 1.0, -.1), Tuple.Vector( 0, 1, 0)},
                {Tuple.Point(  .3,-1.0, -.7), Tuple.Vector( 0,-1, 0)},
                {Tuple.Point( -.6,  .3, 1.0), Tuple.Vector( 0, 0, 1)},
                {Tuple.Point(  .4,  .4,-1.0), Tuple.Vector( 0, 0,-1)},
                {Tuple.Point( 1.0, 1.0, 1.0), Tuple.Vector( 1, 0, 0)},
                {Tuple.Point(-1.0,-1.0,-1.0), Tuple.Vector(-1, 0, 0)},
            };
        }

        [Theory]
        [MemberData(nameof(LocalNormalData1))]
        public void WhereTheNormalOnTheSurfaceOfACube(Tuple point, Tuple normal)
        {
            //Given
            GivenACube(new Cube());
            GivenAPoint(point);

            //When
            WhenLocalNormalAtIsCalled(_cube, _point);

            //Then
            ThenTheNormalIs(normal);
        }

        private void ThenTheNormalIs(Tuple normal)
        {
            _normal.Should().BeEquivalentTo(normal);
        }

        private void WhenLocalNormalAtIsCalled(Cube cube, Tuple point)
        {
            _normal = cube.LocalNormalAt(point);
        }

        private void GivenAPoint(Tuple point)
        {
            _point = point;
        }
    }
}
