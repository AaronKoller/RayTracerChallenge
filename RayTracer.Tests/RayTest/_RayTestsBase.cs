using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace RayTracer.Tests.RayTest
{
    public class _RayTestsBase
    {
        internal Ray _ray;
        internal Ray _resultRay;

        internal void GivenARay(Ray ray)
        {
            _ray = ray;
        }

        internal void ThenTheOriginIs(Tuple origin)
        {
            _resultRay.Origin.Should().Be(origin);
        }

        internal void ThenTheDirectionIs(Tuple direction)
        {
            _resultRay.Direction.Should().Be(direction);

        }
    }
}
