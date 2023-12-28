using RayTracer.Shapes;
using RayTracer.Transformations;

namespace RayTracer.Tests.ShapesTests.SpheresTests
{
    public class _SphereTestsBase
    {
        internal Sphere _sphere;
        internal Matrix _transformMatrix;

        internal void GivenASphere(Sphere sphere)
        {
            _sphere = sphere;
        }

        internal void GivenATranslationTransformation(double x, double y, double z)
        {
            _transformMatrix = Matrix.Transform.Translation(x, y, z);
        }

        internal void GivenTheTransformationIsSetOnASphere(Matrix transformation)
        {
            _sphere.Transform = transformation;
        }
    }
}
