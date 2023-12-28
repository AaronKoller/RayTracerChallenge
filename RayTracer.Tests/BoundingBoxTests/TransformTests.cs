using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using RayTracer.Tests.BoundingBox;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.BoundingBoxTests
{
    public class TransformTests : _boundingBoxTestsBase
    {
        private Matrix _transform;

        [Fact]
        public void TransformingABoundingBox()
        {
            //Given
            GivenABoundingBox(new RayTracer.BoundingBox(Tuple.Point(-1,-1,-1), Tuple.Point(1,1,1)));
            GivenATransformation(Matrix.Transform.Rotation_x(Math.PI / 4) * Matrix.Transform.Rotation_y(Math.PI / 4));

            //When
            WhenTransformIsCalled(_boundingBox, _transform);

            //Then
            ThenTheResultBoundingBoxMinIsToPrecision(.0001, Tuple.Point(-1.4142, -1.7071, -1.7071));
            ThenTheResultBoundingBoxMaxIsToPrecision(.0001, Tuple.Point(1.4142, 1.7071, 1.7071));

        }

        private void ThenTheResultBoundingBoxMaxIsToPrecision(double precision, Tuple point)
        {
            _boundingBoxResult.Max.X.Should().BeApproximately(point.X, precision);
            _boundingBoxResult.Max.Y.Should().BeApproximately(point.Y, precision);
            _boundingBoxResult.Max.Z.Should().BeApproximately(point.Z, precision);
        }

        private void ThenTheResultBoundingBoxMinIsToPrecision(double precision, Tuple point)
        {
            _boundingBoxResult.Min.X.Should().BeApproximately(point.X,precision);
            _boundingBoxResult.Min.Y.Should().BeApproximately(point.Y,precision);
            _boundingBoxResult.Min.Z.Should().BeApproximately(point.Z,precision);
        }

        private void WhenTransformIsCalled(RayTracer.BoundingBox boundingBox, Matrix transform)
        {
            _boundingBoxResult = boundingBox.Transform(transform);
        }

        private void GivenATransformation(Matrix transform)
        {
            _transform = transform;
        }
    }
}
