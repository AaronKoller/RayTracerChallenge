using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.MatrixTests.TransformTests
{
    public class TranslationsTests : _TransformTestsBase
    {
        [Fact]
        public void WhereAPointIsTranslated_ThePointIsMoved()
        {
            //Given
            GivenATranslationTransformation(5, -3, 2);
            GivenATuple(Tuple.Point(-3, 4, 5));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(2, 1, 7));
        }

        [Fact]
        public void WhereAPointIsInverseTranslated_ThePointIsMovedNegatively()
        {
            //Given
            GivenAnInverseTranslationTranformation(5, -3, 2);
            GivenATuple(Tuple.Point(-3, 4, 5));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(-8, 7, 3));
        }

        [Fact]
        public void WhereAVectorIsTranslated_TheSameVectorIsReturned()
        {
            //Given
            GivenATranslationTransformation(5, -3, 2);
            GivenATuple(Tuple.Vector(-3, 4, 5));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Vector(-3, 4, 5));
        }

        private void GivenATranslationTransformation(double x, double y, double z)
        {
            _transformMatrix1 = Matrix.Transform.Translation(x, y, z);
        }

        private void GivenAnInverseTranslationTranformation(double x, double y, double z)
        {
            _transformMatrix1 = Matrix.Transform.Translation(x, y, z).Inverse();
        }
    }
}
