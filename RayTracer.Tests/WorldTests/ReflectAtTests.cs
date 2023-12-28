using RayTracer.Shapes;
using RayTracer.Transformations;
using System;
using Xunit;

namespace RayTracer.Tests.WorldTests
{
    public class ReflectAtTests : _WorldTestsBase
    {
        [Fact]
        public void WhereARayStrikesANonReflectiveSurface_ThenBlackIsReturned()
        {
            //Given
            GivenAWorld(new World());
            GivenARay(new Ray(Tuple.OriginPoint, Tuple.Vector(0, 0, 1)));
            GivenTheSecondShapeInTheWorld(_world.Shapes[1]);
            GivenAnAmbience(_shape1, 1);
            GivenAnIntersection(new Intersection(1, _shape1));
            GivenPreComputation(_intersection, _ray);

            //When
            WhenReflectedColorIsCalled(_world, _precomputation);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.00001, Color.Black);
        }

        [Fact]
        public void WhereTheReflectedColorForAReflectiveMaterial_ThenTheReflectedColorIsReturned()
        {

            double root2 = Math.Sqrt(2);
            //Given
            GivenAWorld(new World());
            GivenAShape(new Plane
            {
                Material = new Material { Reflective = .5 },
                Transform = Matrix.Transform.Translation(0, -1, 0)
            });
            GivenAShapeAddedToTheWorld(_world, _shape1);
            GivenARay(new Ray(RayTracer.Tuple.Point(0, 0, -3), Tuple.Vector(0, -root2 / 2, root2 / 2)));
            GivenAnIntersection(new Intersection(root2, _shape1));
            GivenPreComputation(_intersection, _ray);

            //When
            WhenReflectedColorIsCalled(_world, _precomputation);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.0001, new Color(.19032, .2379, .14274));
        }


        [Fact]
        public void WhereAReflectedColorWithMaximumRecursion_ThenTheColorIsReturned()
        {

            var root2 = Math.Sqrt(2);
            //Given
            GivenAWorld(new World());
            GivenAShape(new Plane
            {
                Material = new Material { Reflective = .5 },
                Transform = Matrix.Transform.Translation(0, -1, 0)
            });
            GivenAShapeAddedToTheWorld(_world, _shape1);
            GivenARay(new Ray(Tuple.Point(0, 0, -3), Tuple.Vector(0, -root2 / 2, root2 / 2)));
            GivenAnIntersection(new Intersection(root2, _shape1));
            GivenPreComputation(_intersection, _ray);

            //When
            WhenReflectedColorIsCalledWithRecursionParameter(_world, _precomputation, 0);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.01, Color.Black);
        }

        private void WhenReflectedColorIsCalledWithRecursionParameter(World world, Intersection.PreComputation precomputation, int recursionDepth)
        {
            _colorResult = world.ReflectedColor(precomputation, recursionDepth);
        }

        private void WhenReflectedColorIsCalled(World world, Intersection.PreComputation precomputation)
        {
            _colorResult = world.ReflectedColor(precomputation);
        }

        private void GivenAnAmbience(Shape shape, double ambience)
        {
            _shape1.Material.Ambient = ambience;
        }

        private void GivenTheSecondShapeInTheWorld(Shape shape)
        {
            _shape1 = shape;
        }

    }
}
