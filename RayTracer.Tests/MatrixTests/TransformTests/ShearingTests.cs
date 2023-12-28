using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.MatrixTests.TransformTests
{
    public class ShearingTests : _TransformTestsBase
    {
        [Theory]
        [InlineData(1,0,0,0,0,0, 5,3,4)]
        [InlineData(0,1,0,0,0,0, 6,3,4)]
        [InlineData(0,0,1,0,0,0, 2,5,4)]
        public void WhereAPointIsTranslated_ThePointIsMoved(
            double shearXy, double shearXz,
            double shearYx, double shearYz,
            double shearZx, double shearZy,
            double outputX, double outputY, double outputZ)
        {
            //Given
            GivenAShearingTransformation(
                shearXy, shearXz,
                shearYx, shearYz,
                shearZx, shearZy);
            GivenATuple(Tuple.Point(2,3,4));

            //When
            WhenTheTupleIsTransformed();

            //Then
            ThenTheTupleIsComputed(Tuple.Point(outputX, outputY, outputZ));
        }

        private void GivenAShearingTransformation(
            double shearXy, double shearXz, 
            double shearYx, double shearYz, 
            double shearZx, double shearZy)
        {
            _transformMatrix1 = Matrix.Transform.Shearing(
                shearXy, shearXz,
                shearYx, shearYz,
                shearZx, shearZy);
        }
    }
}
