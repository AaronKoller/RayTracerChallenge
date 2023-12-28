using Xunit;

namespace RayTracer.Tests.MatrixTests
{
    public class _MatrixFunctionalTests : _MatrixTestsBase
    {

        [Fact]
        public void WhereTheMatrixProductIsMultipliedByItsInverse_TheOriginalMatrixIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                { 3,-9, 7, 3 },
                { 3,-8, 2,-9 },
                {-4, 4, 4, 1 },
                {-6, 5,-1, 1 },
            }));

            GivenMatrix2(new Matrix(new double[,]
            {
                {8, 2, 2, 2 },
                {3,-1, 7, 0 },
                {7, 5, 5, 4 },
                {6,-2, 0, 5 },
            }));

            //When
            WhenMatricesAreMultipliedAndTheInverseIsApplied();

            //Then
            ThenTheResultIsComputedToPrecision(.00000000000001, _matrix1);
        }

        private void WhenMatricesAreMultipliedAndTheInverseIsApplied()
        {
            _resultMatrix = (_matrix1 * _matrix2) * _matrix2.Inverse();
        }
    }
}
