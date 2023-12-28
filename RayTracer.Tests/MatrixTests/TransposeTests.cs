using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests.MatrixTests
{
    public class TransposeTests : _MatrixTestsBase
    {

        [Fact]
        public void WhenTransposeIsCalledOn4x4Matrix_TheResultIsTransposed()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {0,9,3,0 },
                {9,8,0,8 },
                {1,8,5,3 },
                {0,0,5,8 },
            }));

            //When
            WhenTransposeIsCalled1();

            //Then
            ThenTheResultMatrixIsComputed(new Matrix(new double[,]
            {
                {0,9,1,0 },
                {9,8,8,0 },
                {3,0,5,5 },
                {0,8,3,8 },
            }));
        }

        [Fact]
        public void WhenTransposeIsCalledOn1x4Matrix_TheResultIsTransposed()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {0,9,3,0 }

            }));

            //When
            WhenTransposeIsCalled1();

            //Then
            ThenTheResultMatrixIsComputed(new Matrix(new double[,]
            {
                {0 },
                {9 },
                {3 },
                {0 },
            }));
        }

        [Fact]
        public void WhenTransposeIsCalledOn4x1Matrix_TheResultIsTransposed()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {0 },
                {9 },
                {3 },
                {0 },
            }));

            //When
            WhenTransposeIsCalled1();

            //Then
            ThenTheResultMatrixIsComputed(new Matrix(new double[,]
            {
                {0,9,3,0 }
            }));
        }

        [Fact]
        public void WhenTransposeIsCalledOn1x1Matrix_TheResultIsTransposed()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {9 },

            }));

            //When
            WhenTransposeIsCalled1();

            //Then
            ThenTheResultMatrixIsComputed(new Matrix(new double[,]
            {
                {9}
            }));
        }

        [Fact]
        public void WhenTransposeIsCalledOnAnIdentityMatrix_ThenTheSameIdentityMatrixIsReturned()
        {
            //Given
            GivenAnIdentityMatrix();

            //When
            WhenTransposeIsCalled1();

            //Then
            ThenTheResultMatrixIsComputed(_matrix1);
        }

        private void GivenAnIdentityMatrix()
        {
            _matrix1 = new Matrix().GenerateIdentityMatrix();
        }

        private void WhenTransposeIsCalled1()
        {
            _resultMatrix = _matrix1.Transpose();
        }
    }
}
