using FluentAssertions;
using System;
using Xunit;

namespace RayTracer.Tests.CameraTests
{
    public class CameraTests
    {
        private double _fieldOfView;
        private int _vSize;
        private int _hSize;
        private Camera _resultCamera;

        [Fact]
        public void WhereCreatingACamera_ACameraIsReturned()
        {
            //Given
            GivenAHSize(160);
            GivenAVSize(120);
            GivenAFieldOfView(Math.PI / 2);

            //When
            WhenCreatingACamera(_hSize, _vSize, _fieldOfView);

            //Then
            ThenTheFollowingPropertiesAreSet();
        }

        [Theory]
        [InlineData(200, 125)]
        [InlineData(125, 200)]
        public void WhereGivenANewCamera_ThenThePixelSizeIsCalculated(double hSize, double vSize)
        {
            //Given


            //When
            WhenCreatingACamera(hSize, vSize, Math.PI/2);

            //Then
            ThenPixelSizeIsApproximately(.01, .01);
        }

        private void ThenPixelSizeIsApproximately(double pixelSize, double precision)
        {
            _resultCamera.PixelSize.Should().BeApproximately(pixelSize, precision);
        }

        private void ThenTheFollowingPropertiesAreSet()
        {
            _resultCamera.HSize.Should().Be(_hSize);
            _resultCamera.VSize.Should().Be(_vSize);
            _resultCamera.FieldOfView.Should().Be(_fieldOfView);
            _resultCamera.Transform.Data.Should().BeEquivalentTo(new Matrix().Identity.Data);

        }

        private void WhenCreatingACamera(double hSize, double vSize, double fieldOfView)
        {
            _resultCamera = new Camera(hSize, vSize, fieldOfView);
        }

        private void GivenAFieldOfView(double fieldOfView)
        {
            _fieldOfView = fieldOfView;
        }

        private void GivenAVSize(int vSize)
        {
            _vSize = vSize;
        }

        private void GivenAHSize(int hSize)
        {
            _hSize = hSize;
        }
    }
}
