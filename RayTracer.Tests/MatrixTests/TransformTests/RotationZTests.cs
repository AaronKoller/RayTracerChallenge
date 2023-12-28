using RayTracer.Transformations;
using System;
using Xunit;

namespace RayTracer.Tests.MatrixTests.TransformTests
{
    public class RotationZTests : _TransformTestsBase
    {
        [Fact]
        public void WhenRotatingAPointAboutTheZAxis45Degrees_ThePointIsRotated()
        {
            //Given
            GivenAnZAxisRotation(Math.PI / 4);
            GivenATuple(Tuple.Point(0, 1, 0));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(-Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0));
        }

        [Fact]
        public void WhenRotatingAPointAboutTheZAxis90degrees_ThePointIsRotated()
        {
            //Given
            GivenAnZAxisRotation(Math.PI / 2);
            GivenATuple(Tuple.Point(0, 1, 0));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(-1, 0, 0));
        }

        private void GivenAnZAxisRotation(double radians)
        {
            _transformMatrix1 = Matrix.Transform.Rotation_z(radians);

        }
    }
}