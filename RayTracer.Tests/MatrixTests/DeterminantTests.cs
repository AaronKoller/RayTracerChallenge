using System;
using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.MatrixTests
{
    public class DeterminantTests : _MatrixTestsBase
    {
        private double _resultInteger;

        [Fact]
        public void WhereGivenA2x2Matrix_ThenTheDeterminantIsComputed()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                { 1,5},
                { -3,2}
            }));

            //When
            WhenDeterminantIsCalled();

            //Then
            ThenTheDeterminantIs(17);

        }

        [Fact]
        public void WhereGivenA3x3Matrix_ThenTheDeterminantIsComputed()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                { 1,2,6},
                { -5,8,-4},
                { 2,6,4 }
            }));

            //When
            WhenDeterminantIsCalled();

            //Then
            ThenTheDeterminantIs(-196);
        }

        [Fact]
        public void WhereGivenA4x4Matrix_ThenTheDeterminantIsComputed()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {-2,-8,3,5},
                {-3,1,7,3},
                {1,2,-9,6},
                {-6,7,7,-9 }
            }));

            //When
            WhenDeterminantIsCalled();

            //Then
            ThenTheDeterminantIs(-4071);
        }

        [Fact]
        public void WgivenANonsquareMatrix_ThenNonSquareMatrixExceptionIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1,1},
                {2,2},
                {3,3}
            }));

            //When
            WhenDeterminantIsCalledWithException();

            //Then
            ThenNonSquareMatrixExceptionIsReturned("A determinant must be calculated on a matrix with an equal number of rows and columns.");
        }

        [Fact]
        public void WgivenANonsquareMatrix_ThenMatrixSizeTooSmallExceptionIsReturned()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {1}
            }));

            //When
            WhenDeterminantIsCalledWithException();

            //Then
            ThenMatrixSizeTooSmallIsReturned("A determinant must be calculated on a matrix with 2 or more columns and rows.");
        }

        private void ThenMatrixSizeTooSmallIsReturned(string message)
        {
            _exceptionResult.Should().Throw<MatrixSizeTooSmall>().WithMessage(message);
        }

        private void WhenDeterminantIsCalledWithException()
        {
            _exceptionResult = () => {var result = _matrix1.Determinant(); };
        }

        private void ThenNonSquareMatrixExceptionIsReturned(string message)
        {
            _exceptionResult.Should().Throw<NonSquareMatrix>().WithMessage(message);
        }

        private void WhenDeterminantIsCalled()
        {
            _resultInteger = _matrix1.Determinant();
        }


        private void ThenTheDeterminantIs(int determinant)
        {
            _resultInteger.Should().Be(determinant);
        }
    }
}
