using FluentAssertions;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.MatrixTests.TransformTests
{
    public class _TransformTestsBase
    {
        protected Matrix _transformMatrix1;

        protected Tuple _resultTuple;
        protected Tuple _tuple1;


        protected void GivenATuple(Tuple tuple)
        {
            _tuple1 = tuple;
        }

        protected void WhenTheTupleIsTransformed()
        {
            _resultTuple = _transformMatrix1 * _tuple1;
        }

        protected void ThenTheTupleIsComputed(Tuple tuple)
        {
            _resultTuple.Should().Be(tuple);
        }
    }
}
