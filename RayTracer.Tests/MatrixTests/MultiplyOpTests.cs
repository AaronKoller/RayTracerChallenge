using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.MatrixTests
{
    public class MultiplyOpTests : _MatrixTestsBase
    {
        [Fact]
        public void WhereMultiplyOpIsCalled_NewMultipliedMatricyIsCreated()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1,2,3,4 },
                {5,6,7,8 },
                {9,8,7,6 },
                {5,4,3,2 },
            }));
            GivenMatrix2(new Matrix(new double[,]
            {
                {-2,1,2,3},
                {3,2,1,-1},
                {4,3,6,5},
                {1,2,7,8},
            }));

            //When
            WhenMultiplyOpIsCalled();

            //Then
            ThenTheResultMatrixIsComputedFromMultiplyOp(new Matrix(new double[,]
            {
                {20,22,50,48},
                {44,54,114,108},
                {40,58,110,102},
                {16,26,46,42},
            }));
        }

        [Fact]
        public void WhereMultiplyOpIsCalled1_NewMultipliedMatricyIsCreated()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                { 1,2,3,4},
                { 2,4,4,2},
                { 8,6,4,1},
                { 0,0,0,1},
            }));
            GivenMatrix2(new Matrix(new double[,]
            {
                {1},
                {2},
                {3},
                {1},
            }));

            //When
            WhenMultiplyOpIsCalled();

            //Then
            ThenTheResultMatrixIsComputedFromMultiplyOp(new Matrix(new double[,]
            {
                {18},
                {24},
                {33},
                {1},
            }));
        }

        [Fact]
        public void WhereA1x1MatrixIsMultipliedByItsIdentityMatrix_TheSameMatrixIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1},
            }));
            GivenAnIdentityMatrix();

            //When
            WhenMultiplyOpIsCalled();

            //Then
            ThenTheResultMatrixIsComputedFromMultiplyOp(_matrix1);
        }

        [Fact]
        public void WhereA4x4MatrixIsMultipliedByItsIdentityMatrix_TheSameMatrixIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {0,1,2,4 },
                {1,2,4,8 },
                {2,4,8,16 },
                {4,8,16,32 },
            }));
            GivenAnIdentityMatrix();

            //When
            WhenMultiplyOpIsCalled();

            //Then
            ThenTheResultMatrixIsComputedFromMultiplyOp(_matrix1);
        }

        [Fact]
        public void WhereA1x4MatrixIsMultipliedByItsIdentityMatrix_TheSameMatrixIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {0,1,2,4 }
            }));
            GivenAnIdentityMatrix();

            //When
            WhenMultiplyOpIsCalled();

            //Then
            ThenTheResultMatrixIsComputedFromMultiplyOp(_matrix1);
        }

        [Fact]
        public void WhereA4x1MatrixIsMultipliedByItsIdentityMatrix_TheSameMatrixIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {0},
                {1},
                {2},
                {4},
            }));
            GivenAnIdentityMatrix();

            //When
            WhenMultiplyOpIsCalled();

            //Then
            ThenTheResultMatrixIsComputedFromMultiplyOp(_matrix1);
        }

        [Fact]
        public void WhereA4x1MatrixIsMultipliedByItsIdentityMatrixWithSwappedMultiplyOp_TheSameMatrixIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {0},
                {1},
                {2},
                {4},
            }));
            GivenAnIdentityMatrix();
            GivenInputOperatorsAreSwapped();

            //When
            WhenMultiplyOpIsCalled();

            //Then
            ThenTheResultMatrixIsComputedFromMultiplyOp(_matrix2);
        }

        private void GivenInputOperatorsAreSwapped()
        {
            var tempMatrix1 = _matrix1;
            var tempMatrix2 = _matrix2;

            _matrix2 = tempMatrix1;
            _matrix1 = tempMatrix2;
        }

        private void GivenAnIdentityMatrix()
        {
            _matrix2 = _matrix1.Identity;
        }

        private void WhenMultiplyOpIsCalled()
        {
            _resultMatrix = _matrix1 * _matrix2;
        }

        private void ThenTheResultMatrixIsComputedFromMultiplyOp(Matrix matrix)
        {
            _resultMatrix.Data.Should().BeEquivalentTo(matrix.Data);
        }
    }
}
