using FluentAssertions;
using System;
using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class _TupleTestsBase
    {
        protected Tuple _tuple1;
        protected Tuple _tuple2;
        protected Tuple _resultTuple;
        protected Tuple _point1;
        protected Tuple _point2;
        protected Tuple _vector1;
        protected Tuple _vector2;
        protected Tuple _resultVector;
        protected Action _exceptionResult;
        protected double _magnitudeResult;
        protected double _resultDouble;

        //Givens
        protected void GivenATuple1(double x, double y, double z, double w)
        {
            _tuple1 = new Tuple(x, y, z, w);
        }

        protected void GivenATuple2(double x, double y, double z, double w)
        {
            _tuple2 = new Tuple(x, y, z, w);
        }

        protected void GivenAPoint1(double x, double y, double z)
        {
            _point1 = Tuple.Point(x, y, z);
        }

        protected void GivenAPoint2(double x, double y, double z)
        {
            _point2 = Tuple.Point(x, y, z);
        }

        protected void GivenAVector1(double x, double y, double z)
        {
            _vector1 = Tuple.Vector(x, y, z);
        }

        protected void GivenAVector2(double x, double y, double z)
        {
            _vector2 = Tuple.Vector(x, y, z);
        }

        //Thens
        protected void ThenResultShouldBeANewTuple(double x, double y, double z, double w)
        {
            _resultTuple.Should().Be(new Tuple(x, y, z, w));

        }

        protected void ThenANewVectorResultIsCreated(double x, double y, double z)
        {
            _resultVector.Should().Be(Tuple.Vector(x, y, z));

        }


        protected void ThenInvalidOperationWithIsReturnedWithMessage(string message)
        {
            _exceptionResult.Should().Throw<InvalidOperationException>().WithMessage(message);
        }
    }
}
