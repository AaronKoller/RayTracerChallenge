using Xunit;

namespace RayTracer.Tests.TupleTests
{
    public class MultiplyByMatrixOp : _TupleTestsBase
    {
        private Matrix _matrix1;

        [Fact]
        public void WhereMultiplyMatrixByTupleOpIsCalled_TheResultIsComputed()
        {
            //Given
            GivenATuple1(1, 2, 3, 1);
            GivenAMatrix(new Matrix(new double[,]
            {
                {1,2,3,4 },
                {2,4,4,2 },
                {8,6,4,1 },
                {0,0,0,1 },
            }));

            //When
            WhenMultiplyMatrixByTupleOpIsCalled();

            //Then
            ThenResultShouldBeANewTuple(18, 24, 33, 1);
        }


        [Fact]
        public void test()
        {
            //Given
            GivenATuple1(1,2,3,4);
            GivenATupleIdentityMatrix();

            //When
            WhenMultiplyMatrixByTupleOpIsCalled();

            //Then
            ThenResultShouldBeANewTuple(1,2,3,4);
        }

        private void GivenATupleIdentityMatrix()
        {
            _matrix1 = _tuple1.IdentityMatrix;
        }

        private void GivenAMatrix(Matrix matrix)
        {
            _matrix1 = matrix;
        }
        private void WhenMultiplyMatrixByTupleOpIsCalled()
        {
            _resultTuple = _matrix1 * _tuple1;

        }
    }
}
