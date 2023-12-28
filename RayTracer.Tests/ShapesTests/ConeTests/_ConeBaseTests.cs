using RayTracer.Shapes;

namespace RayTracer.Tests.ShapesTests.ConeTests
{
    public class _ConeBaseTests
    {
        internal Shape _shape;
        internal Ray _ray;

        internal void GivenAShape(Shape shape)
        {
            _shape = shape;
        }

        internal void GivenARay(Ray ray)
        {
            _ray = ray;
        }
    }
}
