using System;
using RayTracer.Shapes;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.WorldTests
{
    public class ColorAtTests : _WorldTestsBase
    {
        private Shape _outerSphere;
        private Shape _innerSphere;
        private Shape _shape1;
        private Shape _shape2;

        [Theory]
        [InlineData(0, 1, 0, 0, 0, 0)]  //when the ray misses
        [InlineData(0, 0, 1, 0.38066, 0.47583, 0.2855)] //When the ray hits
        public void WhereARayInTheWorld_ThenItShouldReturnAColor(double vX, double vY, double vZ, double red, double green, double blue)
        {
            //Given
            GivenAWorld(new World());
            GivenARay(new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(vX, vY, vZ)));

            //When
            WhenColorAtIsCalled(_world, _ray);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.00001, new Color(red, green, blue));
        }

        [Fact]
        public void WhereARayStartsBetweenTheSpheresInADefaultWorld_ThenTheColorIntersectionBehindTheRay()
        {
            //Given
            GivenAWorld(new World());
            GivenAnInnerSphere(_world);
            GivenAnOuterSphere(_world);
            GivenAnAmbienceIsSetOnTheSphere(_innerSphere, 1);
            GivenAnAmbienceIsSetOnTheSphere(_outerSphere, 1);
            GivenARay(new Ray(Tuple.Point(0, 0, .75), Tuple.Vector(0, 0, -1)));

            //When
            WhenColorAtIsCalled(_world, _ray);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.00001, _innerSphere.Material.Color);
        }

        [Fact]
        public void WhereThereAreTwoParallelReflectiveSurfaces_ThenTheProgramShouldTerminateSuccessfullyNoInfiniteRecursion()
        {
            //Given
            GivenAWorld(new World());
            GivenALightOnTheWorld(new Light(Tuple.OriginPoint, Color.White));
            GivenAShape1(new Plane
            {
                Material = new Material { Reflective = 1},
                Transform = Matrix.Transform.Translation(0,-1,0)
            });
            GivenAShapeAddedToTheWorld(_world, _shape1);
            GivenAShape2(new Plane
            {
                Material = new Material { Reflective = 1 },
                Transform = Matrix.Transform.Translation(0, 1, 0)
            });
            GivenAShapeAddedToTheWorld(_world, _shape2);
            GivenARay(new Ray(Tuple.OriginPoint, Tuple.Vector(0,1,0)));

            //When
            WhenColorAtIsCalled(_world, _ray);

            //Then
            //Should Terminate successfully
        }


        private void GivenAShape1(Shape shape)
        {
            _shape1 = shape;
        }

        private void GivenAShape2(Shape shape)
        {
            _shape2 = shape;
        }

        private void GivenAnOuterSphere(World world)
        {
            _outerSphere = world.Shapes[0];
        }

        private void GivenAnInnerSphere(World world)
        {
            _innerSphere = world.Shapes[1];
        }

        private void GivenAnAmbienceIsSetOnTheSphere(Shape worldSphere, int ambience)
        {
            worldSphere.Material.Ambient = ambience;
        }

        private void WhenColorAtIsCalled(World world, Ray ray)
        {
            _colorResult = world.ColorAt(ray);
        }
    }
}
