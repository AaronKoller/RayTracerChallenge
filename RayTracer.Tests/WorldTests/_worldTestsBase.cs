using FluentAssertions;
using RayTracer.Shapes;

namespace RayTracer.Tests.WorldTests
{
    public class _WorldTestsBase
    {
        internal World _world;
        internal Ray _ray;
        internal Color _colorResult;
        internal Shape _shape1;
        internal Intersection _intersection;
        internal Intersection.PreComputation _precomputation;

        internal void GivenALightOnTheWorld(Light light)
        {
            _world.Light = light;
        }

        internal void GivenAWorld(World world)
        {
            _world = world;
        }

        internal void GivenARay(Ray ray)
        {
            _ray = ray;
        }

        internal void GivenAShape(Shape shape)
        {
            _shape1 = shape;
        }

        internal void GivenPreComputation(Intersection intersection, Ray ray)
        {
            _precomputation = intersection.PrepareComputations(ray);
        }

        internal void GivenAnIntersection(Intersection intersection)
        {
            _intersection = intersection;
        }

        internal void GivenAShapeAddedToTheWorld(World world, Shape shape)
        {
            _world.Shapes.Add(shape);
        }

        internal void ThenTheColorResultIsReturnedToPrecision(double precision, Color color)
        {
            _colorResult.Should().BeEquivalentTo(color, option =>
                option.Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, precision))
                    .WhenTypeIs<double>());
        }
    }
}
