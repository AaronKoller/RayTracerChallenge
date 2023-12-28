using System;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace RayTracer.Tests
{
    public class _CanvasTestsBase
    {
        protected Canvas _canvas1;
        protected Color _backgroundColor1;
        protected Color _foregroundColor;
        protected int _xCoordinate;
        protected int _yCoordinate;
        protected Action _exceptionResult;
        protected int _height;
        protected int _originOffset;
        protected int _width;
        protected int _xCoordnateFromOrigin;
        protected int _yCoordinateFromOrigin;

        protected void GivenABackgroundColor(Color color)
        {
            _backgroundColor1 = color;
        }

        protected void GivenACanvas(int width, int height, Color color)
        {
            _canvas1 = new Canvas(width, height, color);
        }

        protected void WhenCanvasIsInitialized()
        {
            _canvas1.Create();
        }
    }
}
