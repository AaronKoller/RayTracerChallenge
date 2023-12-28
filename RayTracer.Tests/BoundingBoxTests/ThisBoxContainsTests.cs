using FluentAssertions;
using RayTracer.Tests.BoundingBox;
using Xunit;

namespace RayTracer.Tests.BoundingBoxTests
{
    public class ThisBoxContainsTests : _boundingBoxTestsBase
    {
        private bool _resultIsInBox;
        private RayTracer.BoundingBox _boundingBox1;
        private RayTracer.BoundingBox _boundingBox2;

        public static TheoryData<Tuple, bool> BoxContainingPointData1()
        {
            return new TheoryData<Tuple, bool>
            {
                {Tuple.Point(5,-2,0), true},
                {Tuple.Point(11,4,7), true},
                {Tuple.Point(8,1,3), true},
                {Tuple.Point(3,0,3), false},
                {Tuple.Point(8,-4,3), false},
                {Tuple.Point(8,1,-1), false},
                {Tuple.Point(13,1,3), false},
                {Tuple.Point(8,5,3), false},
                {Tuple.Point(8,1,8), false},
            };
        }

        [Theory]
        [MemberData(nameof(BoxContainingPointData1))]
        public void CheckingToSeeIfABoxContainsAGivenPoint(Tuple point, bool expectedResult)
        {
            //Given
            GivenABoundingBox(new RayTracer.BoundingBox(Tuple.Point(5, -2, 0), Tuple.Point(11, 4, 7)));
            GivenAPoint1(point);


            //When
            WhenBoxContainsPointIsCalled(_boundingBox, _point1);

            //Then
            ThenTheResultShouldBe(expectedResult);
        }


        public static TheoryData<Tuple, Tuple, bool> BoxContainingPointData2()
        {
            return new TheoryData<Tuple, Tuple, bool>
            {
                {Tuple.Point(5,-2,0), Tuple.Point(11,4,7), true},
                {Tuple.Point(6,-1,1), Tuple.Point(10,3,6), true},
                {Tuple.Point(4,-3,-1), Tuple.Point(10,3,6), false},
                {Tuple.Point(6,-1,1), Tuple.Point(12,5,8), false},
            };
        }

        [Theory]
        [MemberData(nameof(BoxContainingPointData2))]
        public void CheckingToSeeIfABoxContainsAGivenBox(Tuple minPoint, Tuple maxPoint, bool expectedResult)
        {
            //Given
            GivenABoundingBox1(new RayTracer.BoundingBox(Tuple.Point(5,-2,0), Tuple.Point(11,4,7)));
            GivenABoundingBox2(new RayTracer.BoundingBox(minPoint, maxPoint));

            //When
            WhenBoxContainsBoxIsCalled(_boundingBox1, _boundingBox2);

            //Then
            ThenTheResultShouldBe(expectedResult);

        }

        private void WhenBoxContainsBoxIsCalled(RayTracer.BoundingBox boundingBox1, RayTracer.BoundingBox boundingBox2)
        {
            _resultIsInBox = boundingBox1.ThisBoxContains(boundingBox2);
        }

        private void GivenABoundingBox2(RayTracer.BoundingBox boundingBox)
        {
            _boundingBox2 = boundingBox;
        }

        private void GivenABoundingBox1(RayTracer.BoundingBox boundingBox)
        {
            _boundingBox1 = boundingBox;
        }

        private void ThenTheResultShouldBe(bool expectedResult)
        {
            _resultIsInBox.Should().Be(expectedResult);
        }

        private void WhenBoxContainsPointIsCalled(RayTracer.BoundingBox boundingBox, Tuple point)
        {
            _resultIsInBox = boundingBox.ThisBoxContains(point);
        }

        private void GivenAPoint1(Tuple point)
        {
            _point1 = point;
        }
    }
}
