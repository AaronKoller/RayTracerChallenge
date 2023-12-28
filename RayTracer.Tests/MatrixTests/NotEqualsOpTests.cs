using Xunit;

namespace RayTracer.Tests.MatrixTests
{
    public class NotEqualsOpTests : _MatrixTestsBase
    {

        [Fact]
        public void WhereTwoIdenticalMatricesWithNotEqualOp_ThenTheyEvaluateToFalsy()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1,1,1,1},
                {1,1,1,1},
                {1,1,1,1},
                {1,1,1,1},
            }));
            GivenMatrix2(new Matrix(new double[,]
            {
                {1,1,1,1},
                {1,1,1,1},
                {1,1,1,1},
                {1,1,1,1},
            }));

            //When
            WhenNotEqualsOpIsCalled();

            //Then
            ThenTheMatricesShouldEvaluateToFalsy();
        }

        [Fact]
        public void WhereTwoNonIdenticalMatricesWithNotEqualOp_ThenTheyEvaluateToTruthy()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1,1,1,1},
                {1,1,1,1},
                {1,1,1,1},
                {1,1,1,1},
            }));
            GivenMatrix2(new Matrix(new double[,]
            {
                {2,2,2,2},
                {2,2,2,2},
                {2,2,2,2},
                {2,2,2,2},
            }));

            //When
            WhenNotEqualsOpIsCalled();

            //Then
            ThenTheMatricesShouldEvaluateToTruthy();
        }

        private void WhenNotEqualsOpIsCalled()
        {
            _resultDoubleEquals = _matrix1 != _matrix2;
        }

    }
}
