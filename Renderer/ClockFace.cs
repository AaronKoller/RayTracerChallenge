using System;
using RayTracer;
using RayTracer.Transformations;
using Tuple = RayTracer.Tuple;

namespace Renderer
{
    public class ClockFace
    {
        private readonly Canvas _canvas;
        private readonly int _divisions;

        public ClockFace(Canvas canvas, int divisions)
        {
            _canvas = canvas;
            _divisions = divisions;
        }

        public void Create()
        {
            var zeroPoint = Tuple.Point(0,0,0);
            var position1 = Matrix.Transform.Translation(1, 0, 0) * zeroPoint;
            var padding = 20;
            var paddedHeight = _canvas.Height - padding;
            var paddedWidth = _canvas.Width - padding;

            for (int i = 0; i < _divisions; i++)
            {
                var rotatedPoint = Matrix.Transform.Rotation_z((Math.PI / 6) * i + 1) * position1;
                var xPosition = (rotatedPoint.X * paddedHeight * .5) + paddedHeight / 2 + padding / 2;
                var yPosition = (rotatedPoint.Y * paddedWidth * .5) + paddedWidth / 2 + padding / 2;
                _canvas.WritePixel(xPosition, yPosition, Color.Red);
            }
        }
    }
}
