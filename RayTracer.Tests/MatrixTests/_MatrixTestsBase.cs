using FluentAssertions;
using System;

namespace RayTracer.Tests.MatrixTests
{
    public class _MatrixTestsBase
    {
        protected Matrix _matrix1;
        protected Matrix _matrix2;
        protected Matrix _resultMatrix;
        protected bool _resultDoubleEquals;
        protected Action _exceptionResult;
        protected double _resultDouble;

        protected void GivenMatrix1(Matrix matrix)
        {
            _matrix1 = matrix;
        }

        protected void GivenMatrix2(Matrix matrix)
        {
            _matrix2 = matrix;
        }



        //protected void WhenMultiplyOpIsCalled()
        //{
        //    _resultMatrix = _matrix1 * _matrix2;
        //}

        protected void ThenTheResultMatrixIsComputed(Matrix matrix)
        {
            _resultMatrix.Data.Should().BeEquivalentTo(matrix.Data);
        }


        protected void ThenTheMatricesShouldEvaluateToFalsy()
        {
            _resultDoubleEquals.Should().BeFalse();
        }

        protected void ThenTheMatricesShouldEvaluateToTruthy()
        {
            _resultDoubleEquals.Should().BeTrue();
        }

        protected void ThenTheCofactorIsComputed(double cofactor)
        {
            _resultDouble.Should().Be(cofactor);
        }

        protected void ThenTheResultIsComputedToPrecision(double precision, Matrix matrix)
        {
            _resultMatrix.Data.Should().BeEquivalentTo(matrix.Data, option =>
                option.Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, precision))
                    .WhenTypeIs<double>());
        }
    }
}
