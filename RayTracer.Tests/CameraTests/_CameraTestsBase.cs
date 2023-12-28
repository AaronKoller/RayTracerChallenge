using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Tests.CameraTests
{
    public class _CameraTestsBase
    {
        internal Camera _camera;

        internal void GivenACamera(double hSize, double vSize, double fieldOfView)
        {
            _camera = new Camera(hSize, vSize, fieldOfView);
        }
    }
}
