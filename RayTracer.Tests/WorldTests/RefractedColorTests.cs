using System;
using RayTracer.Patterns;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.WorldTests
{
    public class RefractedColorTests : _WorldTestsBase
    {
        private Intersections _intersections;
        private Shape _shape1;
        private Shape _shape2;

        [Fact]
        public void WhereTheRefractedColorWithAnOpaqueSurface_ThenBlackIsReturned()
        {
            //Given
            GivenAWorld(new World());
            GivenAShape1FromTheWorld(_world.Shapes[0]);
            GivenTheShape1HasTheMaterial(new Material
            {
                Transparency = 1.0,
                RefractiveIndex = 1.5
            });
            GivenARay(new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1)));
            GivenIntersections(new Intersections
            {
                [0] = new Intersection(4, _shape1),
                [1] = new Intersection(6, _shape1),
            });
            GivenPrepareComputationsWithIntersections(_intersections[0], _ray, _intersections);

            //When
            WhenRefractedColorIsCalled(_world, _precomputation, 0);

            //Then
            ThenTheColorResultIsReturnedToPrecision(0, Color.Black);
        }

        [Fact]
        public void WhereTheRefractedColorUnderTotalInternalReflection_IsBlack()
        {
            var number = Math.Sqrt(2)/2;
            //Given
            GivenAWorld(new World());
            GivenAShape1FromTheWorld(_world.Shapes[0]);
            GivenTheShape1HasTheMaterial(new Material
            {
                Transparency = 1.0,
                RefractiveIndex = 1.5
            });
            GivenARay(new Ray(Tuple.Point(0, 0, number), Tuple.Vector(0, 1, 0)));
            GivenIntersections(new Intersections
            {
                [0] = new Intersection(-number, _shape1),
                [1] = new Intersection( number, _shape1),
            });
            GivenPrepareComputationsWithIntersections(_intersections[1], _ray, _intersections);

            //When
            WhenRefractedColorIsCalled(_world, _precomputation, 5);

            //Then
            ThenTheColorResultIsReturnedToPrecision(0, Color.Black);
        }


        [Fact]
        public void WhereTheRayIsRefracted_ThenTheRefractedColorIsReturned()
        {
            //Given
            GivenAWorld(new World());
            GivenAShape1FromTheWorld(_world.Shapes[0]);
            GivenTheShape1HasTheMaterial(new Material
            {
                Ambient = 1.0,
                Pattern = new TestPattern()
            });
            GivenAShape2FromTheWorld(_world.Shapes[1]);
            GivenTheShape2HasTheMaterial(new Material
            {
                Transparency = 1.0,
                RefractiveIndex = 1.5
            });
            GivenARay(new Ray(Tuple.Point(0, 0, .1), Tuple.Vector(0, 1, 0)));
            GivenIntersections(new Intersections
            {
                [0] = new Intersection(-.9899, _shape1),
                [1] = new Intersection(-.4899, _shape2),
                [2] = new Intersection( .4899, _shape2),
                [3] = new Intersection( .9899, _shape1),
            });
            GivenPrepareComputationsWithIntersections(_intersections[2], _ray, _intersections);

            //When
            WhenRefractedColorIsCalled(_world, _precomputation, 5);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.0001, new Color(0,.99888, .04725));
        }

        private void GivenTheShape2HasTheMaterial(Material material)
        {
            _shape2.Material = material;
        }

        private void GivenAShape2FromTheWorld(Shape shape)
        {
            _shape2 = shape;
        }

        private void WhenRefractedColorIsCalled(World world, Intersection.PreComputation precomputation, int recursiveDepth)
        {
            _colorResult = world.RefractedColor(precomputation, recursiveDepth);
        }

        private void GivenPrepareComputationsWithIntersections(Intersection intersection, Ray ray, Intersections intersections)
        {
            _precomputation = intersection.PrepareComputations(ray, intersections);
        }

        private void GivenIntersections(Intersections intersections)
        {
            _intersections = intersections;
        }

        private void GivenTheShape1HasTheMaterial(Material material)
        {
            _shape1.Material = material;
        }

        private void GivenAShape1FromTheWorld(Shape shape)
        {
            _shape1 = shape;
        }
    }
}
