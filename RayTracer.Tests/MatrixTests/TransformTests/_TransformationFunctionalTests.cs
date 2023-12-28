using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.MatrixTests.TransformTests
{
   public class _TransformationFunctionalTests : _TransformTestsBase
    {
        [Fact]
        public void MultipleTransformations_ResultsInCorrectTransformation()
        {

            //Given
            GivenATuple(Tuple.Point(1,0,1));

            //Operations
            GivenAXRotationTransformation(Math.PI / 2);
            WhenTheTupleIsTransformedByPrevious(_tuple1);
            ThenTheTupleIsComputed(Tuple.Point(1,-1,0));

            GivenAScalingTransformation(5,5,5);
            WhenTheTupleIsTransformedByPrevious(_resultTuple);
            ThenTheTupleIsComputed(Tuple.Point(5, -5, 0));

            GivenATranslationTransformation(10,5,7);
            WhenTheTupleIsTransformedByPrevious(_resultTuple);
            ThenTheTupleIsComputed(Tuple.Point(15, 0, 7));
        }

        [Fact]
        public void MultipleTransformationsFluent_ResultsInCorrectTransformation()
        {
            //Matrix operations are not communicative, therefore the order of operations is important.
            //Additionally when performing multiple operations you must go in reverse order
            //A matrix that rotates, then scales, then translates then in code you must translate, then scale, then rotate to get the correct result.

            //Given
            GivenATuple(Tuple.Point(1, 0, 1));
            GivenARotationScalingTranslationTransformation();

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(15,0,7));
        }

        private void GivenARotationScalingTranslationTransformation()
        {
            _transformMatrix1 = Matrix.Transform.Translation(10, 5, 7) *
                                Matrix.Transform.Scale(5, 5, 5) *
                                Matrix.Transform.Rotation_x(Math.PI / 2);
        }

        private void GivenATranslationTransformation(double x, double y , double z)
        {
            _transformMatrix1 = Matrix.Transform.Translation(x, y, z);

        }

        private void WhenTheTupleIsTransformedByPrevious(Tuple tuple)
        {
            _resultTuple = _transformMatrix1 * tuple;
        }

        private void GivenAScalingTransformation(double scaleX, double scaleY, double scaleZ)
        {
            _transformMatrix1 = Matrix.Transform.Scale(scaleX, scaleY, scaleZ);
        }

        private void GivenAXRotationTransformation(double radians)
        {
            _transformMatrix1 = Matrix.Transform.Rotation_x(radians);
        }
    }
}
