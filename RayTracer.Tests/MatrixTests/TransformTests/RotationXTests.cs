using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.MatrixTests.TransformTests
{
    public class RotationXTests : _TransformTestsBase
    {
        [Fact]
        public void WhenRotatingAPointAboutTheXAxis45Degrees_ThePointIsRotated()
        {
            //Given
            GivenAnXAxisRotation(Math.PI/4);
            GivenATuple(Tuple.Point(0,1,0));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(0,Math.Sqrt(2)/2, Math.Sqrt(2)/2));
        }

        [Fact]
        public void WhenRotatingAPointAboutTheXAxis90degrees_ThePointIsRotated()
        {
            //Given
            GivenAnXAxisRotation(Math.PI / 2);
            GivenATuple(Tuple.Point(0, 1, 0));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(0, 0, 1));
        }

        [Fact]
        public void WhenRotatingAPointAboutTheXAxis90degreesNegatively_ThePointIsRotatedNegatively()
        {
            //Given
            GivenANegativeXAxisRotation(Math.PI / 4);
            GivenATuple(Tuple.Point(0, 1, 0));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2));
        }

        private void GivenANegativeXAxisRotation(double radians)
        {
            _transformMatrix1 = Matrix.Transform.Rotation_x(radians).Inverse();

        }

        private void GivenAnXAxisRotation(double radians)
        {
            _transformMatrix1 = Matrix.Transform.Rotation_x(radians);

        }
    }
}
