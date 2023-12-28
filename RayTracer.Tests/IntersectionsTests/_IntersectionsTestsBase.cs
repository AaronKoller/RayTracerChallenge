using RayTracer.Shapes;

namespace RayTracer.Tests.IntersectionsTests
{
    public class _IntersectionsTestsBase
    {
        internal Shape _shape;
        internal Intersection _givenIntersection0;
        internal Intersection _givenIntersection1;
        internal Intersection _givenIntersection2;
        internal Intersection _givenIntersection3;

        internal void GivenAShape(Shape shape)
        {
            _shape = shape;
        }

        internal void GivenAnIntersection0(Intersection intersection)
        {
            _givenIntersection0 = intersection;
        }

        internal void GivenAnIntersection1(Intersection intersection)
        {
            _givenIntersection1 = intersection;
        }

        internal void GivenAnIntersection2(Intersection intersection)
        {
            _givenIntersection2 = intersection;
        }

        internal void GivenAnIntersection3(Intersection intersection)
        {
            _givenIntersection3 = intersection;
        }
    }
}
