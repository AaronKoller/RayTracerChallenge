using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.MatrixTests
{
    public class CoFactorTests : _MatrixTestsBase
    {

        [Fact]
        public void WhereCofactorIsCalledOnA3X3MatrixAndColumnAndRowSumToEven_ThenTheCofatorIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {3,5,0 },
                {2,-1,-7 },
                {6,-1,5 },
            }));

            //When
            WhenCofactorIsCalledOnA3X3Matrix(0, 0);

            //Then
            ThenTheCofactorIsComputed(-12);
        }

        [Fact]
        public void WhereCofactorIsCalledOnA3X3MatrixAndColumnAndRowSumToOdd_ThenTheCofatorIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {3,5,0 },
                {2,-1,-7 },
                {6,-1,5 },
            }));

            //When
            WhenCofactorIsCalledOnA3X3Matrix(1, 0);

            //Then
            ThenTheCofactorIsComputed(-25);
        }

        private void WhenCofactorIsCalledOnA3X3Matrix(int excludedRow, int excludedColumn)
        {
            _resultDouble = _matrix1.CoFactor(excludedRow, excludedColumn);
        }


    }
}
