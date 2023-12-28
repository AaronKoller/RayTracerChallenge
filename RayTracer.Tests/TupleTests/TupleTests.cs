using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class TupleTests : _TupleTestsBase
    {
        [Fact]
        public void WhereATupleWithWEquals1_ThenTupleIsAPoint()
        {
            //Given
            var tuple = new Tuple(4.3, -4.2, 3.1, 1.0);

            //Then
            tuple.X.Should().Be(4.3);
            tuple.Y.Should().Be(-4.2);
            tuple.Z.Should().Be(3.1);
            tuple.W.Should().Be(1.0);
            tuple.IsPoint.Should().BeTrue();
            tuple.IsVector.Should().BeFalse();
        }

        [Fact]
        public void WhereATupleWithWEquals0_ThenTupleIsAVector()
        {
            //Given
            var a = new Tuple(4.3, -4.2, 3.1, 0.0);

            //Then
            a.X.Should().Be(4.3);
            a.Y.Should().Be(-4.2);
            a.Z.Should().Be(3.1);
            a.W.Should().Be(0.0);
            a.IsPoint.Should().BeFalse();
            a.IsVector.Should().BeTrue();
        }

        [Fact]
        public void WhereAPoint_ThenShouldBeATupleWhereWIs1()
        {
            //Given
            GivenAPoint1(4, 4, 3);

            //Then
            ThenPoint1ShouldBeATuple(4, 4, 3, 1);
        }

        [Fact]
        public void WhereAVector_ThenShouldBeATupleWhereWIs0()
        {
            //Given
            GivenAVector1(4, 4, 3);

            //Then
            ThenVector1ShouldBeATuple(4, 4, 3, 0);
        }

        [Fact]
        public void WhereAZeroVector_ThenShouldBeTuple0000()
        {
            //Given
            GivenAZeroVectorResult();

            //Then
            ThenResultShouldBeANewTuple(0, 0, 0, 0);
        }


        [Fact]
        public void WhereAOriginPoint_ShouldReturnATuple0001()
        {
            //Given


            //When
            WhenAnOriginPointIsCreated();

            //Then
            ThenPoint1ShouldBeATuple(0,0,0,1);

        }

        private void WhenAnOriginPointIsCreated()
        {
            _point1 = Tuple.OriginPoint;
        }

        public void GivenAZeroVectorResult()
        {
            _resultTuple = Tuple.Zero;
        }



        public void ThenPoint1ShouldBeATuple(double x, double y, double z, double w)
        {
            _point1.Should().Be(new Tuple(x, y, z, w));
        }

        public void ThenVector1ShouldBeATuple(double x, double y, double z, double w)
        {
            _vector1.Should().Be(new Tuple(x, y, z, w));
        }

    }
}
