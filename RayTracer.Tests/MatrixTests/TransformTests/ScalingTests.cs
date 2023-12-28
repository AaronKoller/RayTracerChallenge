using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.MatrixTests.TransformTests
{
    public class ScalingTests : _TransformTestsBase
    {

        [Fact]
        public void WhereAPointIsScaled_ThenAPointIsScaled()
        {
            //Given
            GivenAScaleTransformation(2, 3, 4);
            GivenATuple(Tuple.Point(-4, 6, 8));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(-8, 18, 32));
        }

        [Fact]
        public void WhereAVectorIsScaled_ThenAVectorIsScaled()
        {
            //Given
            GivenAScaleTransformation(2, 3, 4);
            GivenATuple(Tuple.Vector(-4, 6, 8));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Vector(-8, 18, 32));
        }

        [Fact]
        public void WhereAVectorIsInverseScaled_ThenAVectorIsScaledNegatively()
        {
            //Given
            GivenAnInverseScaleTransformation(2, 3, 4);
            GivenATuple(Tuple.Vector(-4, 6, 8));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Vector(-2, 2, 2));
        }

        [Fact]
        public void WhereAPointIsReflectedAcrossTheXAxis_ThenTheOppositeXIsReturned()
        {
            //Given
            GivenAnXAxisTransformation();
            GivenATuple(Tuple.Point(2,3,4));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(-2,3,4));
        }

        private void GivenAnXAxisTransformation()
        {
            _transformMatrix1 = Matrix.Transform.Scale(-1, 1, 1);
        }

        private void GivenAnInverseScaleTransformation(double x, double y, double z)
        {
            _transformMatrix1 = Matrix.Transform.Scale(x, y, z).Inverse();

        }

        private void GivenAScaleTransformation(double x, double y, double z)
        {
            _transformMatrix1 = Matrix.Transform.Scale(x, y, z);
        }
    }
}
