using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.CameraTests
{
    public class RayForPixelTests : _CameraTestsBase
    {
        private Ray _resultRay;

        [Fact]
        public void WhereThroughTheCenterOfTheCanvas_ARayIsReturned()
        {
            //Given
            GivenACamera(201,101, Math.PI / 2);

            //When
            WhenRayForPixelIsCalled(_camera, 100, 50);

            //Then
            ThenTheRayPropertiesAre(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, -1));
        }

        [Fact]
        public void WhereThroughTheCornerOfTheCanvas_ARayIsReturned()
        {
            //Given
            GivenACamera(201, 101, Math.PI / 2);

            //When
            WhenRayForPixelIsCalled(_camera, 0, 0);

            //Then
            ThenTheRayPropertiesAre(Tuple.Point(0, 0, 0), Tuple.Vector(.66519, .33259, -.66851));
        }

        [Fact]
        public void WhereTheCameraIsTransformed_ARayIsReturned()
        {
            //Given
            GivenACamera(201, 101, Math.PI / 2);
            GivenACameraTransformation(Matrix.Transform.Rotation_y(Math.PI/4) * Matrix.Transform.Translation(0,-2,5));

            //When
            WhenRayForPixelIsCalled(_camera, 100, 50);

            //Then

            var number = Math.Sqrt(2) / 2;
            ThenTheRayPropertiesAre(Tuple.Point(0, 2, -5), Tuple.Vector(number, 0, -number));
        }

        private void GivenACameraTransformation(Matrix transform)
        {
            _camera.Transform = transform;
        }

        private void ThenTheRayPropertiesAre(Tuple point, Tuple vector)
        {
            _resultRay.Origin.Should().Be(point);
            _resultRay.Direction.Should().Be(vector);
        }

        private void WhenRayForPixelIsCalled(Camera camera, double pixelX, double pixelY)
        {
            _resultRay = _camera.RayForPixel(pixelX, pixelY);
        }


    }
}
