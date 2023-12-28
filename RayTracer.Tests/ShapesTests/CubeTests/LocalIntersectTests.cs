using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.CubeTests
{
    public class LocalIntersectTests : _CubeBaseTests
    {
        private Intersections _intersections;

        public static TheoryData<Tuple, Tuple, int, int> LocalIntersectData1()
        {
            return new TheoryData<Tuple, Tuple, int, int>
            {
                {Tuple.Point(5,.5,0), Tuple.Vector(-1,0,0),4,6},
                {Tuple.Point(-5,.5,0), Tuple.Vector(1,0,0),4,6},
                {Tuple.Point(.5,5,0), Tuple.Vector(0,-1,0),4,6},
                {Tuple.Point(.5,-5,0), Tuple.Vector(0,1,0),4,6},
                {Tuple.Point(.5,0,5), Tuple.Vector(0,0,-1),4,6},
                {Tuple.Point(.5,0,-5), Tuple.Vector(0,0,1),4,6},
                {Tuple.Point(0,.5,0), Tuple.Vector(0,0,1),-1,1},
            };
        }

        [Theory]
        [MemberData(nameof(LocalIntersectData1))]
        public void WhereARayIntersectsWithACube(Tuple origin, Tuple direction, int t1, int t2)
        {
            //Given
            GivenACube(new Cube());
            GivenARay(new Ray(origin, direction));

            //When
            WhenLocalIntersectIsCalled(_cube, _ray);

            //Then
            ThenTheIntersectionCountIs(2);
            ThenTheTValueAtIntersectionArrayValueIs(0, t1);
            ThenTheTValueAtIntersectionArrayValueIs(1, t2);
        }


        public static TheoryData<Tuple, Tuple> LocalIntersectData2()
        {
            return new TheoryData<Tuple, Tuple>
            {
                {Tuple.Point(-2,0,0), Tuple.Vector(.2673,.5345,.8018)},
                {Tuple.Point(0,-2,0), Tuple.Vector(.8018,.2673,.5345)},
                {Tuple.Point(0,0,-2), Tuple.Vector(.5345,.8018,.2673)},
                {Tuple.Point(2,0,2), Tuple.Vector(0,0,-1)},
                {Tuple.Point(0,2,2), Tuple.Vector(0,-1,0)},
                {Tuple.Point(2,2,0), Tuple.Vector(-1,0,0)},

            };
        }

        [Theory]
        [MemberData(nameof(LocalIntersectData2))]
        public void WhereARayMissesACube(Tuple origin, Tuple direction)
        {
            //Given
            GivenACube(new Cube());
            GivenARay(new Ray(origin, direction));

            //When
            WhenLocalIntersectIsCalled(_cube, _ray);

            //Then
            ThenTheIntersectionCountIs(0);
        }

        private void ThenTheTValueAtIntersectionArrayValueIs(int arrayValue, int t)
        {
            _intersections[arrayValue].T.Should().Be(t);
        }

        private void ThenTheIntersectionCountIs(int count)
        {
            _intersections.Count.Should().Be(count);
        }

        private void WhenLocalIntersectIsCalled(Cube cube, Ray ray)
        {
            _intersections = cube.LocalIntersect(ray);
        }
    }
}
