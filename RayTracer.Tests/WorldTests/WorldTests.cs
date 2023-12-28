using FluentAssertions;
using RayTracer.Shapes;
using RayTracer.Transformations;
using System;
using System.Linq;
using Xunit;

namespace RayTracer.Tests.WorldTests
{
    public class WorldTests : _WorldTestsBase
    {
        private World _worldResult;
        private Light _light;
        private Sphere _sphere1;
        private Sphere _sphere2;
        private Intersections _intersectionsResult;

        [Fact]
        public void WhereANewWorld_ThenTheWorldHasADefaultLightAndObjects()
        {
            //Given
            GivenALight(new Light(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)));
            GivenASphere1();
            GivenASphere2();

            //When
            WhenAWorldIsCreated();

            //Then
            ThenTheWordContainsALight(_light);
            ThenTheWorldContainsSphere1();
            ThenTheWorldContainsSphere2();
        }

        [Fact]
        public void WhereADefaultWorldWithARay_IntersectionsAreReturned()
        {
            //Given
            GivenAWorld(new World());
            GivenARay(new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1)));

            //When
            WhenARayIntersectsWithAWorld(_ray);

            //Then
            ThenTheIntersectionCountIs(4);
            ThenTheIntersectionIs(4.0);
            ThenTheIntersectionIs(4.5);
            ThenTheIntersectionIs(5.5);
            ThenTheIntersectionIs(6.0);
        }

        private void GivenALight(Light light)
        {
            _light = light;
        }

        private void GivenASphere1()
        {
            _sphere1 = new Sphere
            {
                Material = new Material { Color = new Color(.8, 1.0, 0.6), Diffuse = .7, Specular = 0.2 }
            };
        }

        private void GivenASphere2()
        {
            _sphere2 = new Sphere { Transform = Matrix.Transform.Scale(.5, .5, .5) };
        }

        private void WhenARayIntersectsWithAWorld(Ray ray)
        {
            _intersectionsResult = _world.IntersectWorld(ray);
        }

        private void WhenAWorldIsCreated()
        {
            _worldResult = new World();
        }

        private void ThenTheIntersectionCountIs(int expectedCounted)
        {
            _intersectionsResult.Count.Should().Be(expectedCounted);
        }

        private void ThenTheIntersectionIs(double intersectionPoint)
        {
            _intersectionsResult.Any(x => Math.Abs(x.T - intersectionPoint) < Constants.EPSILON).Should().BeTrue();
        }

        private void ThenTheWordContainsALight(Light light)
        {
            _worldResult.Light.Should().BeEquivalentTo(light);
        }

        private void ThenTheWorldContainsSphere1()
        {
            _worldResult.Shapes.First().Material.Should().BeEquivalentTo(_sphere1.Material);
        }

        private void ThenTheWorldContainsSphere2()
        {
            _worldResult.Shapes.Last().Transform.Data.Should().BeEquivalentTo(_sphere2.Transform.Data);
        }
    }
}
