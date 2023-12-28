using FluentAssertions;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.MatrixTests
{
    public class ViewTransformTests : _MatrixTestsBase
    {
        private Tuple _from;
        private Tuple _to;
        private Tuple _up;

        [Fact]
        public void WhereADefaultViewTransformDefined_TheIdentityMatrixIsReturned()
        {
            //Given
            GivenAFrom(Tuple.Point(0, 0, 0));
            GivenATo(Tuple.Point(0, 0, -1));
            GivenAnUp(Tuple.Vector(0, 1, 0));

            //When
            WhenViewTransformIsCalled(_from, _to, _up);

            //Then
            ThenTheMatrixTransformationIsReturned(new Matrix().Identity);
        }

        [Fact]
        public void WhereLookingInAPositiveZDirection_ThenTheViewIsAMirrorImage()
        {
            //Given
            GivenAFrom(Tuple.Point(0, 0, 0));
            GivenATo(Tuple.Point(0, 0, 1));
            GivenAnUp(Tuple.Vector(0, 1, 0));

            //When
            WhenViewTransformIsCalled(_from, _to, _up);

            //Then
            ThenTheMatrixTransformationIsReturned(new Matrix().Scale(-1,1,-1));
        }

        [Fact]
        public void WhereGivenAViewTranslation_TheViewMovesTheWorld()
        {
            //Given
            GivenAFrom(Tuple.Point(0, 0, 8));
            GivenATo(Tuple.Point(0, 0, 0));
            GivenAnUp(Tuple.Vector(0, 1, 0));

            //When
            WhenViewTransformIsCalled(_from, _to, _up);

            //Then
            ThenTheMatrixTransformationIsReturned(new Matrix().Translation(0, 0, -8));
        }

        [Fact]
        public void WhereGivenAnArbitraryView_TheAShearingScaledTranslationTransformationIsReturned()
        {
            //Given
            GivenAFrom(Tuple.Point(1,3,2));
            GivenATo(Tuple.Point(4,-2,8));
            GivenAnUp(Tuple.Vector(1, 1, 0));

            //When
            WhenViewTransformIsCalled(_from, _to, _up);

            //Then
            ThenTheResultIsComputedToPrecision(.0001, new Matrix(new [,]
            {
                {-0.50709, 0.50709,  0.67612, -2.36643},
                { 0.76772, 0.60609,  0.12122, -2.82843},
                {-0.35857, 0.59761, -0.71714,  0      },
                { 0      , 0      ,  0      ,  1      },
            }));
        }


        private void GivenAFrom(Tuple from)
        {
            _from = from;
        }

        private void GivenATo(Tuple to)
        {
            _to = to;
        }

        private void GivenAnUp(Tuple up)
        {
            _up = up;
        }

        private void WhenViewTransformIsCalled(Tuple from, Tuple to, Tuple up)
        {
            _resultMatrix = new Matrix().ViewTransform(from, to, up);
        }

        private void ThenTheMatrixTransformationIsReturned(Matrix identity)
        {
            _resultMatrix.Data.Should().BeEquivalentTo(identity.Data);
        }
    }
}
