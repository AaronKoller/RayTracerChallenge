using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.MatrixTests
{
    public class SubMatrix : _MatrixTestsBase
    {
        [Fact]
        public void WhereSubMatrixIsCalledOnA3x3Matrix_ThenThe2x2SubMatrixIsComputed()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1,5,0 },
                {-3,2,7 },
                {0,6,-3 },
            }));

            //When
            WhenSubMatrixIsCalled(0,2);

            //Then
            ThenTheResultSubMatrixIsComputed(new Matrix(new double[,]
            {
                {-3,2 },
                {0,6 },
            }));
        }

        [Fact]
        public void WhereSubMatrixIsCalledOnA4x4Matrix_ThenThe3x3SubMatrixIsComputed()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {-6,1,1,6 },
                {-8,5,8,6 },
                {-1,0,8,2 },
                {-7,1,-1,1 },
            }));

            //When
            WhenSubMatrixIsCalled(2,1);

            //Then
            ThenTheResultSubMatrixIsComputed(new Matrix(new double[,]
            {
                { -6,1,6},
                { -8,8,6},
                {-7,-1,1 },
            }));
        }

        [Fact]
        public void WhereSubMatrixIsCalledOnAMatrixWithOneColumn_ThenMatrixSizeTooSmallExceptionIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1},
                {-3},
                {0},
            }));

            //When
            WhenSubMatrixIsCalledWithException(0,0);

            //Then
            ThenMatrixSizeTooSmallExceptionIsThrown("The submatrix can not be performed on a matrix that has one column or one row");
        }

        [Fact]
        public void WhereSubMatrixIsCalledOnAMatrixWithOneRow_ThenMatrixSizeTooSmallExceptionIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1, 3, 0},
            }));

            //When
            WhenSubMatrixIsCalledWithException(0, 0);

            //Then
            ThenMatrixSizeTooSmallExceptionIsThrown("The submatrix can not be performed on a matrix that has one column or one row");
        }

        [Fact]
        public void WhereSubMatrixIsCalledOnAMatrixWithOneRowAndOneColumn_ThenMatrixSizeTooSmallExceptionIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1},
            }));

            //When
            WhenSubMatrixIsCalledWithException(0, 0);

            //Then
            ThenMatrixSizeTooSmallExceptionIsThrown("The submatrix can not be performed on a matrix that has one column or one row");
        }

        [Theory]
        [InlineData(4,2)]
        [InlineData(2,4)]
        public void WhereSubMatrixIsCalledWithAnExcludedColumnOrRowThatIsTooBig_ThenAnExceptionIsReturned(int excludedRow, int excludedColumn)
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1,5,0 },
                {-3,2,7 },
                {0,6,-3 },
            }));

            //When
            WhenSubMatrixIsCalledWithException(excludedRow, excludedColumn);

            //Then
            ThenMatrixExclusionColumnOrRowTooBigExceptionIsThrown("The excluded column or row was of an index larger then the matrix being converted into a submatrix.");
        }

        private void ThenMatrixExclusionColumnOrRowTooBigExceptionIsThrown(string message)
        {
            _exceptionResult.Should().Throw<ExclusionColumnOrRowTooBig>().WithMessage(message);

        }

        private void ThenMatrixSizeTooSmallExceptionIsThrown(string message)
        {
            _exceptionResult.Should().Throw<MatrixSizeTooSmall>().WithMessage(message);
        }

        private void ThenTheResultSubMatrixIsComputed(Matrix matrix)
        {
            _resultMatrix.Data.Should().BeEquivalentTo(matrix.Data);
        }

        private void WhenSubMatrixIsCalledWithException(int excludedRow, int excludedColumn)
        {
            _exceptionResult = () => {var result = _matrix1.SubMatrix(excludedRow, excludedColumn); };
        }

        private void WhenSubMatrixIsCalled(int excludedRow, int excludedColumn)
        {

            _resultMatrix = _matrix1.SubMatrix(excludedRow, excludedColumn);
        }
    }
}
