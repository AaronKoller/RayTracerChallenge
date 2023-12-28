using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Xunit;

namespace RayTracer.Tests
{
    public class _ColorTestsBase
    {
        protected Color _color1;
        protected Color _color2;
        protected Color _resultColor;

        protected void GivenColor2IsCreated(double red, double green, double blue)
        {
            _color2 = new Color(red, green ,blue);
        }

        protected void GivenColor1IsCreated(double red, double green, double blue)
        {
            _color1 = new Color(red, green, blue);
        }

        protected void ThenResultColorIs(double red, double green, double blue)
        {
            _resultColor.Should().BeEquivalentTo(new Color(red, green, blue), 
                options => options
                    .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, Constants.EPSILON))
                    .WhenTypeIs<double>());
        }
    }
}
