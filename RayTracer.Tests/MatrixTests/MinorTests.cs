using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.MatrixTests
{
    public class Minor : _MatrixTestsBase
    {
        private double _resultDouble;

        [Fact]
        public void WhenMinorIsCalledOn3x3Matrix_TheMinorIsComputed()
        {
            //Given
            GivenMatrix1(new Matrix(new double[,]
            {
                {3,5,0 },
                {2,-1,-7 },
                {6,-1,5 },
            }));

            //When
            WhenMinorIsCalled(1, 0);

            //Then
            ThenTheMinorIsComputed(25);
        }


        private void WhenMinorIsCalled(int excludedRow, int excludedColumn)
        {
            _resultDouble = _matrix1.Minor(excludedRow, excludedColumn);
        }


        private void ThenTheMinorIsComputed(int minor)
        {
            _resultDouble.Should().Be(minor);
        }
    }
}
