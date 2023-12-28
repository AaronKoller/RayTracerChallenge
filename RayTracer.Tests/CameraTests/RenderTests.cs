using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.CameraTests
{
    public class RenderTests : _CameraTestsBase
    {
        private World _world;
        private Tuple _up;
        private Tuple _to;
        private Tuple _from;
        private Canvas _resultImage;

        [Fact]
        public void WhereRenderingWithACamera_TheWorldIsRendered()
        {
            //Given
            GivenAWorld(new World());
            GivenACamera(11,11,Math.PI/2);
            GivenAFrom(Tuple.Point(0,0,-5));
            GivenATo(Tuple.Point(0,0,0));
            GivenAnUp(Tuple.Vector(0, 1, 0));
            GivenACameraTransform(_from, _to, _up);

            //When
            WhenRenderIsCalled(_camera, _world);

            //Then
            ThenTheColorIsReturnedAtPixel(.0001, 5, 5, new Color(0.38066, .47583, .2855));
        }

        private void ThenTheColorIsReturnedAtPixel(double precision, int x, int y, Color color)
        {

            _resultImage.Data[x, y].Should().BeEquivalentTo(color, option =>
                option.Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, precision))
                    .WhenTypeIs<double>());

        }

        private void WhenRenderIsCalled(Camera camera, World world)
        {
            _resultImage = _camera.Render(_world);
        }

        private void GivenACameraTransform(Tuple from, Tuple to, Tuple up)
        {
            _camera.Transform = new Matrix().ViewTransform(from, to, up);
        }

        private void GivenAnUp(Tuple up)
        {
            _up = up;
        }

        private void GivenATo(Tuple to)
        {
            _to = to;
        }

        private void GivenAFrom(Tuple from)
        {
            _from = from;
        }

        private void GivenAWorld(World world)
        {
            _world = world;
        }
    }
}
