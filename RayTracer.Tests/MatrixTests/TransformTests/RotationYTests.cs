using RayTracer.Transformations;
using System;
using Xunit;

namespace RayTracer.Tests.MatrixTests.TransformTests
{
    public class RotationYTests : _TransformTestsBase
    {
        [Fact]
        public void WhenRotatingAPointAboutTheYAxis45Degrees_ThePointIsRotated()
        {
            //Given
            GivenAnYAxisRotation(Math.PI / 4);
            GivenATuple(Tuple.Point(0, 0, 1));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2));
        }

        [Fact]
        public void WhenRotatingAPointAboutTheYAxis90degrees_ThePointIsRotated()
        {
            //Given
            GivenAnYAxisRotation(Math.PI / 2);
            GivenATuple(Tuple.Point(0, 0, 1));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(1, 0, 0));
        }


        private void GivenAnYAxisRotation(double radians)
        {
            _transformMatrix1 = Matrix.Transform.Rotation_y(radians);

        }
    }
}