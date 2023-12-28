using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.RayTest
{
    public class MultiplyOpTests : _RayTestsBase
    {
        private Matrix _transformMatrix1;

        [Fact]
        public void WhereARayHasATranslationTransform_ItHasANewOrigin()
        {
            //Given
            GivenARay(new Ray(Tuple.Point(1,2,3), Tuple.Vector(0, 1, 0)));
            GivenATranslationTransformation(3, 4, 5);

            //When
            WhenTheRayIsTransformed();

            //Then
            ThenTheOriginIs(Tuple.Point(4,6,8));
            ThenTheDirectionIs(Tuple.Vector(0,1,0));
        }


        [Fact]
        public void WhereARayHasAScalingTransform_ItHasANewOriginAndDirection()
        {
            //Given
            GivenARay(new Ray(Tuple.Point(1, 2, 3), Tuple.Vector(0, 1, 0)));
            GivenAScalingTransformation(2,3,4);

            //When
            WhenTheRayIsTransformed();

            //Then
            ThenTheOriginIs(Tuple.Point(2,6,12));
            ThenTheDirectionIs(Tuple.Vector(0,3,0));
        }

        private void GivenAScalingTransformation(double x, double y, double z)
        {
            _transformMatrix1 = Matrix.Transform.Scale(x, y, z);
        }


        private void GivenATranslationTransformation(double x, double y, double z)
        {
            _transformMatrix1 = Matrix.Transform.Translation(x, y, z);
        }

        protected void WhenTheRayIsTransformed()
        {
            _resultRay = _transformMatrix1 * _ray;
        }
    }
}
