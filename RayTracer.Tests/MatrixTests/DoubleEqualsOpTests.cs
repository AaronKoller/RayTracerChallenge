using System;
using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.MatrixTests
{
    public class DoubleEqualsOpTests : _MatrixTestsBase

    {

        [Fact]
        public void WhereTwoIdenticalMatricesWithDoubleEqualOp_ThenTheyEvaluateToTruthy()
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
            WhenDoubleEqualsOpIsCalled();

            //Then
            ThenTheMatricesShouldEvaluateToTruthy();
        }

        [Fact]
        public void WhereTwoNonIdenticalMatricesWithDoubleEqualOp_ThenTheyEvaluateToFalsy()
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
            WhenDoubleEqualsOpIsCalled();

            //Then
            ThenTheMatricesShouldEvaluateToFalsy();
        }

        [Fact]
        public void WhereTwoMatricesHaveUnequalColumns_ThenOpMatrixSizeExceptionOccurs()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1, 1},
                {1, 1}
            }));
            GivenMatrix2(new Matrix(new double[,]
            {
                {1, 1, 1},
                {1, 1, 1}
            }));

            //When
            WhenDoubleEqualsOpIsCalledWithException();

            //Then
            ThenExceptionIsReturnedWithMessage("The matrices do not have the same number of columns.");
        }

        [Fact]
        public void WhereTwoMatricesHaveUnequalRows_ThenOpMatrixSizeExceptionOccurs()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1, 1},
                {1, 1}
            }));
            GivenMatrix2(new Matrix(new double[,]
            {
                {1, 1},
                {1, 1},
                {1, 1}
            }));

            //When
            WhenDoubleEqualsOpIsCalledWithException();

            //Then
            ThenExceptionIsReturnedWithMessage("The matrices do not have the same number of rows.");
        }


        private void WhenDoubleEqualsOpIsCalled()
        {
            _resultDoubleEquals = _matrix1 == _matrix2;
        }

        private void WhenDoubleEqualsOpIsCalledWithException()
        {
            _exceptionResult = () => { var result = _matrix1 == _matrix2; };
        }

        private void ThenExceptionIsReturnedWithMessage(string message)
        {
            _exceptionResult.Should().Throw<MatrixSizeMismatch>().WithMessage(message);
        }
    }
}
