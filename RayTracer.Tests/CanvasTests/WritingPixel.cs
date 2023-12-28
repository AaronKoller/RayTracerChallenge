using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.CanvasTests
{
    public class WritingPixel : _CanvasTestsBase
    {
        [Theory]
        [InlineData(0, 0, 4, 4)]
        [InlineData(0, 3, 4, 4)]
        [InlineData(3, 0, 4, 4)]
        [InlineData(3, 3, 4, 4)]
        public void WhereWritingPixelValueWithINtValues_ColorIsChangedAtPixel(int x, int y, int width, int height)
        {
            //Given
            GivenAXCoordinate(x);
            GivenAYCoordinate(y);
            GivenABackgroundColor(Color.Black);
            GivenAForegroundColor(Color.Red);
            GivenAHeightAndWidth(width, height);
            GivenACanvas(_width, _height, _backgroundColor1);
            WhenCanvasIsInitialized();
            GivenOffSetCoordinate();

            //When
            WhenPixelColorIsChangedWithIntCoordinates(_xCoordinate, _yCoordinate, _foregroundColor);

            //Then
            ThenPixelsIsNewColor(_xCoordnateFromOrigin, _yCoordinateFromOrigin, _foregroundColor);
        }

        [Theory]
        [InlineData(0, 0, 4, 4)]
        public void WhereWritingPixelValueWithDoubleValues_ColorIsChangedAtPixel(double x, double y, int width, int height)
        {
            //Given
            GivenAXCoordinate((int)x);
            GivenAYCoordinate((int)y);
            GivenABackgroundColor(Color.Black);
            GivenAForegroundColor(Color.Red);
            GivenAHeightAndWidth(width, height);
            GivenACanvas(_width, _height, _backgroundColor1);
            WhenCanvasIsInitialized();
            GivenOffSetCoordinate();

            //When
            WhenPixelColorIsChangedWithDoubleCoordinates(_xCoordinate, _yCoordinate, _foregroundColor);

            //Then
            ThenPixelsIsNewColor(_xCoordnateFromOrigin, _yCoordinateFromOrigin, _foregroundColor);
        }

     
        [Theory]
        [InlineData(-1, -1, 4, 4)]
        [InlineData(-1, 0, 4, 4)]
        [InlineData(0, -1, 4, 4)]
        [InlineData(4, 0, 4, 4)]
        [InlineData(0, 4, 4, 4)]
        [InlineData(4, 4, 4, 4)]
        public void WhereWritingPixelValueOutsideOfCanvas_ThenNoValuesAreChangedInTheCanvas(int x, int y, int width, int height)
        {
            //Given
            GivenAXCoordinate(x);
            GivenAYCoordinate(y);
            GivenABackgroundColor(Color.Black);
            GivenAForegroundColor(Color.Red);
            GivenACanvas(width, height, _backgroundColor1);
            WhenCanvasIsInitialized();

            //When
            WhenPixelColorIsChangedWithIntCoordinates(_xCoordinate, _yCoordinate, _foregroundColor);

            //Then
            ThenNoColorIsChangedInTheCanvas();
        }

        [Fact]
        public void GivenColorsThatAreTooTooSmall_ThenWritePixelsClampsToBlack()
        {
            //Given
            GivenAXCoordinate((int)0);
            GivenAYCoordinate((int)0);
            GivenABackgroundColor(Color.Black);
            GivenAForegroundColor(Color.Black);
            GivenAHeightAndWidth(4, 4);
            GivenACanvas(_width, _height, _backgroundColor1);
            WhenCanvasIsInitialized();
            GivenOffSetCoordinate();

            //When
            WhenPixelColorIsChangedWithDoubleCoordinates(_xCoordinate, _yCoordinate, new Color(-1,-1,-1));

            //Then
            ThenPixelsIsNewColor(_xCoordnateFromOrigin, _yCoordinateFromOrigin, _foregroundColor);
        }

        [Fact]
        public void GivenColorsThatAreTooTooBig_ThenWritePixelsClampsToWhite()
        {
            //Given
            GivenAXCoordinate((int)0);
            GivenAYCoordinate((int)0);
            GivenABackgroundColor(Color.Black);
            GivenAForegroundColor(Color.White);
            GivenAHeightAndWidth(4, 4);
            GivenACanvas(_width, _height, _backgroundColor1);
            WhenCanvasIsInitialized();
            GivenOffSetCoordinate();

            //When
            WhenPixelColorIsChangedWithDoubleCoordinates(_xCoordinate, _yCoordinate, new Color(2, 2, 2));

            //Then
            ThenPixelsIsNewColor(_xCoordnateFromOrigin, _yCoordinateFromOrigin, _foregroundColor);
        }

        protected void GivenAHeightAndWidth(int height, int width)
        {
            _height = height;
            _width = width;
            _originOffset = _height - 1;
        }

        protected void GivenOffSetCoordinate()
        {
            _xCoordnateFromOrigin = _xCoordinate;
            _yCoordinateFromOrigin = _originOffset - _yCoordinate;
        }

        protected void GivenAYCoordinate(int y)
        {
            _yCoordinate = y;
        }

        protected void GivenAXCoordinate(int x)
        {
            _xCoordinate = x;
        }

        protected void GivenAForegroundColor(Color color)
        {
            _foregroundColor = color;
        }


        protected void WhenPixelColorIsChangedWithIntCoordinates(int x, int y, Color color)
        {
            _canvas1.WritePixel(x, y, color);

        }

        private void WhenPixelColorIsChangedWithDoubleCoordinates(double x, double y, Color color)
        {
            _canvas1.WritePixel(x, y, color);
        }

        protected void ThenPixelsIsNewColor(int xCoordinate, int yCoordinate, Color color)
        {
            _canvas1.Data[xCoordinate, yCoordinate].Should().BeEquivalentTo(color);

        }



        protected void ThenNoColorIsChangedInTheCanvas()
        {
            for (int x = 0; x < _canvas1.Data.GetLength(0); x++)
            {
                for (int y = 0; y < _canvas1.Data.GetLength(1); y++)
                {
                    _canvas1.Data[x, y].Should().NotBe(_foregroundColor);
                }
            }
        }

    }
}
