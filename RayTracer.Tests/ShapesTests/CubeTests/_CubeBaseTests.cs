using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Shapes;

namespace RayTracer.Tests.ShapesTests.CubeTests
{
    public class _CubeBaseTests
    {
        internal Cube _cube;
        internal Ray _ray;

        internal void GivenACube(Cube cube)
        {
            _cube = cube;
        }

        internal void GivenARay(Ray ray)
        {
            _ray = ray;
        }
    }
}
