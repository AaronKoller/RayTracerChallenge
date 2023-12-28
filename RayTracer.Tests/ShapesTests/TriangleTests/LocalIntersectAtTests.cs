using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using RayTracer.Shapes;
using Xunit;
using Xunit.Sdk;

namespace RayTracer.Tests.ShapesTests.TriangleTests
{
    public class LocalIntersectAtTests : _TriangleBaseTests
    {
        private Ray _ray;
        private Intersections _resultIntersections;

        [Fact]
        public void IntersectingARayParallelToTheTriangle()
        {
            //Given
            GivenATriangle(new Triangle(Tuple.Point(0,1,0), Tuple.Point(-1,0,0), Tuple.Point(1,0,0)));
            GivenARay(new Ray(Tuple.Point(0,-1,-2), Tuple.Vector(0,1,0)));

            //When
            WhenLocalIntersectIsCalled(_triangle, _ray);

            //Then
            ThenTheIntersectionIsEmpty();
        }

        [Fact]
        public void ARayMissesTheP1P3Edge()
        {
            //Given
            GivenATriangle(new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0), Tuple.Point(1, 0, 0)));
            GivenARay(new Ray(Tuple.Point(1, 1, -2), Tuple.Vector(0, 0, 1)));

            //When
            WhenLocalIntersectIsCalled(_triangle, _ray);

            //Then
            ThenTheIntersectionIsEmpty();
        }

        [Fact]
        public void ARayMissesTheP1P2Edge()
        {
            //Given
            GivenATriangle(new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0), Tuple.Point(1, 0, 0)));
            GivenARay(new Ray(Tuple.Point(-1, 1, -2), Tuple.Vector(0, 0, 1)));

            //When
            WhenLocalIntersectIsCalled(_triangle, _ray);

            //Then
            ThenTheIntersectionIsEmpty();
        }

        [Fact]
        public void ARayMissesTheP2P3Edge()
        {
            //Given
            GivenATriangle(new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0), Tuple.Point(1, 0, 0)));
            GivenARay(new Ray(Tuple.Point(0, -1, -2), Tuple.Vector(0, 0, 1)));

            //When
            WhenLocalIntersectIsCalled(_triangle, _ray);

            //Then
            ThenTheIntersectionIsEmpty();
        }

        [Fact]
        public void ARayStrikesATriangle()
        {
            //Given
            GivenATriangle(new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0), Tuple.Point(1, 0, 0)));
            GivenARay(new Ray(Tuple.Point(0, .5, -2), Tuple.Vector(0, 0, 1)));

            //When
            WhenLocalIntersectIsCalled(_triangle, _ray);

            //Then
            ThenIntersectCountIs(1);
            ThenTheX0IntersectPositionIs(2);
        }

        private void ThenTheX0IntersectPositionIs(int i)
        {
            _resultIntersections[0].T.Should().Be(i);
        }

        private void ThenIntersectCountIs(int i)
        {
            _resultIntersections.Count.Should().Be(i);
        }

        private void ThenTheIntersectionIsEmpty()
        {
            _resultIntersections.Should().BeNull();
        }

        private void WhenLocalIntersectIsCalled(Triangle triangle, Ray ray)
        {
            _resultIntersections = triangle.LocalIntersect(ray);
        }

        private void GivenARay(Ray ray)
        {
            _ray = ray;
        }
    }
}
