using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class SubtractOpTests : _TupleTestsBase
    {
        [Fact]
        public void WhereTwoPointsAreSubtracted_ThenAVectorIsCreated()
        {
            //Given
            GivenAPoint1(3, 2, 1);
            GivenAPoint2(5, 6, 7);

            //When
            WhenSubtractOpIsCalled();

            //Then
            ThenVector1IsCreated(-2, -4, -6);
        }

        [Fact]
        public void WhereAVectorIsSubtractedFromAPoint_ThenANewPointIsCreated()
        {
            //Given
            GivenAPoint1(3, 2, 1);
            GivenAVector1(5, 6, 7);

            //When
            WhenVector1IsSubtractedFromAPoint1();

            //Then
            ThenSubtractedPoint2ShouldBeANewPoint(-2, -4, -6);
        }

        [Fact]
        public void WhereAVectorIsSubtractedFromAVector_ThenANewVectorIsCreated()
        {
            //Given
            GivenAVector1(3, 2, 1);
            GivenAVector2(5, 6, 7);

            //When
            WhenVector2IsSubtractedFromVector1();

            //Then
            ThenANewVectorResultIsCreated(-2, -4, -6);
        }

        [Fact]
        public void WhereAVectorIsSubtractedFromAPoint_ThenAnInvalidOperationExceptionIsThrown()
        {
            //Given
            GivenAVector1(3, 2, 1);
            GivenAPoint1(5, 6, 7);

            //When
            WhenSubtractWithExceptionVectorFromPointIsCalled();

            //Then
            ThenInvalidOperationWithIsReturnedWithMessage("A vector cannot be subtracted from a point.");
        }

        [Fact]
        public void WhereVector1SubtractedFromZeroVector_TheOppositeVector1Returned()
        {
            //Given
            GivenAZeroVector();
            GivenAVector2(1, -2, 3);

            //When
            WhenVector2IsSubtractedFromVector1();

            //Then
            ThenResultShouldBeANewVector(-1, 2, -3);

        }

        public void GivenAZeroVector()
        {
            _vector1 = Tuple.Zero;
        }

        public void WhenVector1IsSubtractedFromAPoint1()
        {
            _point2 = _point1 - _vector1;
        }

        public void WhenSubtractWithExceptionVectorFromPointIsCalled()
        {
            _exceptionResult = () => { var vectorResult = _vector1 - _point1; };
        }


        public void WhenSubtractOpIsCalled()
        {
            _vector1 = _point1 - _point2;
        }

        public void ThenVector1IsCreated(double x, double y, double z)
        {
            _vector1.Should().Be(Tuple.Vector(x, y, z));
        }

        public void ThenSubtractedPoint2ShouldBeANewPoint(double x, double y, double z)
        {
            _point2.Should().Be(Tuple.Point(x, y, z));

        }

        public void WhenVector2IsSubtractedFromVector1()
        {
            _resultVector = _vector1 - _vector2;
        }

        public void ThenResultShouldBeANewVector(double x, double y, double z)
        {
            _resultVector.Should().Be(Tuple.Vector(x, y, z));
        }

    }
}
