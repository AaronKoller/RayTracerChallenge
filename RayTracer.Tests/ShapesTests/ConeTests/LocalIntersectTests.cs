using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.ConeTests
{
    public class LocalIntersectTests : _ConeBaseTests
    {
        private Tuple _direction;
        private Intersections _intersections;

        public static TheoryData<Tuple, Tuple> LocalIntersectData1()
        {
            return new TheoryData<Tuple, Tuple>
            {
                {Tuple.Point(0,0,-1), Tuple.Vector(0,1,-1)},
            };
        }

        [Theory]
        [MemberData(nameof(LocalIntersectData1))]
        public void WhereIntersectingAConeWithARayParallelToOneOfItsHalves_ShouldReturnOneIntersection(Tuple origin, Tuple direction)
        {
            //Given
            GivenAShape(new Cone());
            GivenADirection(direction.Normalize());
            GivenARay(new Ray(origin, _direction));

            //When
            WhenLocalIntersectIsCalled(_shape, _ray);

            //Then
            ThenTheIntersectCountIs(1);
        }

        public static TheoryData<Tuple, Tuple, double, double> LocalIntersectData2()
        {
            return new TheoryData<Tuple, Tuple, double, double>
            {
                {Tuple.Point(0,0,-5), Tuple.Vector(0,0,1), 5,5},
                {Tuple.Point(0,0,-5), Tuple.Vector(1,1,1), 8.66025,8.66025},
                {Tuple.Point(1,1,-5), Tuple.Vector(-.5,-1,1), 4.55006,49.44994},
            };
        }

        [Theory]
        [MemberData(nameof(LocalIntersectData2))]
        public void WhereARayStrikesACylinder_ThenIntersectionsAreReturned(Tuple origin, Tuple direction, double t0, double t1)
        {
            //Given
            GivenAShape(new Cone());
            GivenADirection(direction.Normalize());
            GivenARay(new Ray(origin, _direction));

            //When
            WhenLocalIntersectIsCalled(_shape, _ray);

            //Then
            ThenTheIntersectCountIs(2);
            ThenTheIntersectionTIsWithinPrecision(.00001, 0, t0);
            ThenTheIntersectionTIsWithinPrecision(.00001, 1, t1);
        }

        public static TheoryData<Tuple, Tuple, int> LocalIntersectData4()
        {
            return new TheoryData<Tuple, Tuple, int>
            {
                {Tuple.Point(0,0,-5), Tuple.Vector( 0,1,0),0},
                {Tuple.Point(0,0,-.25), Tuple.Vector( 0,1,1),2},
                {Tuple.Point(0,0,-.25), Tuple.Vector( 0,1,0),4},
            };
        }
        [Theory]
        [MemberData(nameof(LocalIntersectData4))]
        public void WhereIntersectingAConesEndcaps_ThenIntersectionsAreReturnedAtConstraints(Tuple point, Tuple direction, int count)
        {
            //Given
            GivenAShape(new Cone
            {
                Minimum = -.5,
                Maximum = .5,
                Closed = true
            });
            GivenADirection(direction.Normalize());
            GivenARay(new Ray(point, _direction));

            //When
            WhenLocalIntersectIsCalled(_shape, _ray);

            //Then
            ThenTheIntersectCountIs(count);
        }


        private void ThenTheIntersectionTIsWithinPrecision(double precision, int index, double t)
        {
            _intersections[index].T.Should().BeApproximately(t, precision);
        }


        private void ThenTheIntersectCountIs(int count)
        {
            _intersections.Count.Should().Be(count);
        }

        private void WhenLocalIntersectIsCalled(Shape shape, Ray ray)
        {
            _intersections = shape.LocalIntersect(ray);
        }

        private void GivenADirection(Tuple direction)
        {
            _direction = direction;
        }
    }
}
