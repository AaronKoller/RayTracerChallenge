using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.CanvasTests
{
    public class CanvasTests : _CanvasTestsBase
    {
        [Fact]
        public void WhereCanvasIsCreated_CanvasIsBlank()
        {
            //Given
            GivenABackgroundColor(Color.Black);
            GivenACanvas(10, 20, _backgroundColor1);

            //When
            WhenCanvasIsInitialized();

            //Then
            ThenCanvasIsBlack(_backgroundColor1);
        }

        protected void ThenCanvasIsBlack(Color color1)
        {
            for (int x = 0; x < _canvas1.Data.GetLength(0); x++)
            {
                for (int y = 0; y < _canvas1.Data.GetLength(1); y++)
                {
                    _canvas1.Data[x, y].Should().Be(_backgroundColor1);
                }
            }
        }
    }
}
