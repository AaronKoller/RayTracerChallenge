using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.IntersectionsTests
{
    public class SchlickTests : _IntersectionsTestsBase
    {

        private double _root2 = Math.Sqrt(2);
        private Ray _ray;
        private Intersections _intersections;
        private Intersection.PreComputation _precomputations;
        private double _reflectance;

        [Fact]
        public void TheSchlickApproximationUnderTotalInternalReflection()
        {
            //Given
            GivenAShape(new GlassSphere());
            GivenARay(new Ray(Tuple.Point(0, 0, _root2 / 2), Tuple.Vector(0, 1, 0)));
            GivenIntersections(new Intersections
            {
                [0] = new Intersection(-_root2 / 2, _shape),
                [1] = new Intersection(_root2 / 2, _shape)
            });
            GivenPrecomputationsWithIntersections(_intersections[1], _ray, _intersections);

            //When
            WhenSchlickIsCalled(_precomputations);

            //Then
            ThenReflectanceIsToPrecision(.01, 1.0);
        }

        [Fact]
        public void TheSchlickApproximationWithAPerpendicularViewingAngle()
        {
            //Given
            GivenAShape(new GlassSphere());
            GivenARay(new Ray(Tuple.OriginPoint, Tuple.Vector(0, 1, 0)));
            GivenIntersections(new Intersections
            {
                [0] = new Intersection(-1, _shape),
                [1] = new Intersection(1, _shape),
            });
            GivenPrecomputationsWithIntersections(_intersections[1], _ray, _intersections);

            //When
            WhenSchlickIsCalled(_precomputations);

            //Then
            ThenReflectanceIsToPrecision(.01, .04);
        }

        [Fact]
        public void TheSchlickApproximationWithASmallViewingAngleAndN2GreaterThanN1()
        {
            //Given
            GivenAShape(new GlassSphere());
            GivenARay(new Ray(Tuple.Point(0,.99,-2), Tuple.Vector(0, 0, 1)));
            GivenIntersections(new Intersections
            {
                [0] = new Intersection(1.8589, _shape),
            });
            GivenPrecomputationsWithIntersections(_intersections[0], _ray, _intersections);

            //When
            WhenSchlickIsCalled(_precomputations);

            //Then
            ThenReflectanceIsToPrecision(.000001, .48873);

        }

        private void ThenReflectanceIsToPrecision(double precision, double refelectance)
        {
            _reflectance.Should().BeApproximately(refelectance, precision);
        }

        private void WhenSchlickIsCalled(Intersection.PreComputation precomputations)
        {
            _reflectance = Intersections.Schlick(precomputations);
        }

        private void GivenPrecomputationsWithIntersections(Intersection intersection, Ray ray, Intersections intersections)
        {
            _precomputations = intersection.PrepareComputations(ray, intersections);
        }

        private void GivenIntersections(Intersections intersections)
        {
            _intersections = intersections;
        }

        private void GivenARay(Ray ray)
        {
            _ray = ray;
        }
    }
}
