using System;
using FluentAssertions;
using RayTracer.Shapes;
using RayTracer.Tests.IntersectionsTests;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.IntersectionTests
{
    public class PrepareComputationsTests : _IntersectionsTestsBase
    {
        private Ray _ray;
        private Intersection.PreComputation _precomputationResult;
        private Shape _shape1;
        private Shape _shape2;
        private Shape _shape3;
        private Intersections _intersections;

        [Fact]
        public void WherePrecomputingTheStateOfAnIntersection_APrecomputationIsReturned()
        {
            //Given
            GivenARay(new Ray(Tuple.Point(0,0,-5), Tuple.Vector(0,0,1)));
            GivenAShape(new Sphere());
            GivenAnIntersection0(new Intersection(4, _shape));

            //When
            WhenPreparePrecomputationIsCalled(_givenIntersection0, _ray);

            //Then
            ThenThePreComputationsAreCalculated(_givenIntersection0);
        }

        [Fact]
        public void WherePrecomputingTheStateOfAnIntersectionOnOutsideOfSphere_APrecomputationIsReturnedOutside()
        {
            //Given
            GivenARay(new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1)));
            GivenAShape(new Sphere());
            GivenAnIntersection0(new Intersection(4, _shape));

            //When
            WhenPreparePrecomputationIsCalled(_givenIntersection0, _ray);

            //Then
            ThenTheIntersectionIsInside();
        }

        [Fact]
        public void WherePrecomputingTheStateOfAnIntersectionOnTheInsideOfASphere_APrecomputationIsReturnedInide()
        {
            //Given
            GivenARay(new Ray(Tuple.OriginPoint, Tuple.Vector(0, 0, 1)));
            GivenAShape(new Sphere());
            GivenAnIntersection0(new Intersection(1, _shape));

            //When
            WhenPreparePrecomputationIsCalled(_givenIntersection0, _ray);

            //Then
            ThenTheIntersectionIsOutside();
        }

        [Fact]
        public void WhereTheHitShouldOffsetTheOverPointByEPSILON()
        {
            //Given
            GivenARay(new Ray(Tuple.Point(0,0,-5),Tuple.Vector(0,0,1)));
            GivenAShape(new Sphere());
            GivenATransformOnShape(_shape, Matrix.Transform.Translation(0, 0, 1));
            GivenAnIntersection0(new Intersection(5, _shape));

            //When
            WhenPreparePrecomputationIsCalled(_givenIntersection0, _ray);

            //Then
            ThenPrecomputationsOverpointIsLessThan(-Constants.EPSILON / 2);
            ThenPointZIsLessThanOverPointZ();
        }

        [Fact]
        public void WhereTheHitShouldOffsetTheUnderPointByEPSILON()
        {
            //Given
            GivenARay(new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1)));
            GivenAShape(new GlassSphere
            {
                Transform = Matrix.Transform.Translation(0,0,1)
            });
            GivenAnIntersection0(new Intersection(5, _shape));
            GivenIntersections(new Intersections
            {
                [0] = _givenIntersection0
            });

            //When
            WhenPreparePrecomputationIsCalledWithIntersections(_givenIntersection0, _ray, _intersections);

            //Then
            ThenPrecomputationsUnderPointIsGreaterThan(Constants.EPSILON / 2);
            ThenPointZIsLessThanUnderPointZ();
        }

        [Fact]
        public void WherePrecomputingTheReflectionVector()
        {
            var number = Math.Sqrt(2) / 2;
            //Given
            GivenAShape(new Plane());
            GivenARay(new Ray(Tuple.Point(0,1,-1), Tuple.Vector(0,-number, number)));
            GivenAnIntersection0(new Intersection(Math.Sqrt(2), _shape));
            //When
            WhenPreparePrecomputationIsCalled(_givenIntersection0, _ray);

            //Then
            ThenThePreCompReflectVectorIs(Tuple.Vector(0,number,number));
        }


        public static TheoryData<int, double, double> memberData()
        {
            return new TheoryData<int, double, double>
            {
                { 0, 1.0, 1.5},
                { 1, 1.5, 2.0},
                { 2, 2.0, 2.5},
                { 3, 2.5, 2.5},
                { 4, 2.5, 1.5},
                { 5, 1.5, 1.0},
            };
        }

        [Theory]
        [MemberData(nameof(memberData))]
        public void WhereAtVariousIntersections_N1AndN2AreFound(int intersection, double n1, double n2)
        {
            //Given
            GivenAShape1(new Sphere
            {
                Transform = Matrix.Transform.Scale(2,2,2),
                Material = new Material { RefractiveIndex = 1.5}
            });
            GivenAShape2(new Sphere
            {
                Transform = Matrix.Transform.Translation(0, 0, -.25),
                Material = new Material { RefractiveIndex = 2.0 }
            });
            GivenAShape3(new Sphere
            {
                Transform = Matrix.Transform.Translation(0, 0, .25),
                Material = new Material { RefractiveIndex = 2.5 }
            });
            GivenARay(new Ray(Tuple.Point(0,0,-4),Tuple.Vector(0,0,1) ));
            GivenIntersections(new Intersections
            {
                [0] = new Intersection(2.00, _shape1),
                [1] = new Intersection(2.75, _shape2),
                [2] = new Intersection(3.25, _shape3),
                [3] = new Intersection(4.75, _shape2),
                [4] = new Intersection(5.25, _shape3),
                [5] = new Intersection(6.00, _shape1),
            });

            //When
            WhenPreparePrecomputationIsCalledWithIntersections(_intersections[intersection], _ray, _intersections);

            //Then
            ThenThePreCompN1Is(n1);
            ThenThePreCompN2Is(n2);
        }

        private void GivenIntersections(Intersections intersections)
        {
            _intersections = intersections;
        }

        private void GivenAShape1(Shape shape)
        {
            _shape1 = shape;
        }
        private void GivenAShape2(Shape shape)
        {
            _shape2 = shape;
        }
        private void GivenAShape3(Shape shape)
        {
            _shape3 = shape;
        }

        private void GivenATransformOnShape(Shape shape, Matrix transform)
        {
            _shape.Transform = transform;
        }

        private void GivenARay(Ray ray)
        {
            _ray = ray;
        }

        private void WhenPreparePrecomputationIsCalled(Intersection intersection, Ray ray)
        {
            _precomputationResult = intersection.PrepareComputations(ray);
        }

        private void WhenPreparePrecomputationIsCalledWithIntersections(Intersection intersection, Ray ray, Intersections intersections)
        {
            _precomputationResult = intersection.PrepareComputations(ray, intersections);
        }

        private void ThenThePreCompReflectVectorIs(Tuple vector)
        {
            _precomputationResult.ReflectVector.Should().Be(vector);
        }

        private void ThenPointZIsLessThanOverPointZ()
        {
            _precomputationResult.Point.Z.Should().BeGreaterThan(_precomputationResult.OverPoint.Z);
        }

        private void ThenPointZIsLessThanUnderPointZ()
        {
            _precomputationResult.Point.Z.Should().BeLessThan(_precomputationResult.UnderPoint.Z);
        }

        private void ThenPrecomputationsOverpointIsLessThan(double d)
        {
            _precomputationResult.OverPoint.Z.Should().BeLessThan(d);
        }

        private void ThenPrecomputationsUnderPointIsGreaterThan(double epsilon)
        {
            _precomputationResult.UnderPoint.Z.Should().BeGreaterThan(epsilon);
        }

        private void ThenTheIntersectionIsInside()
        {
            _precomputationResult.Inside.Should().BeFalse();
            }

        private void ThenTheIntersectionIsOutside()
        {
            _precomputationResult.Point.Should().Be(Tuple.Point(0, 0, 1));
            _precomputationResult.EyeVector.Should().Be(Tuple.Vector(0, 0, -1));
            _precomputationResult.Inside.Should().BeTrue();
            _precomputationResult.NormalVector.Should().Be(Tuple.Vector(0, 0, -1));
        }


        private void ThenThePreComputationsAreCalculated(Intersection intersection)
        {
            _precomputationResult.T.Should().Be(intersection.T);
            _precomputationResult.Shape.Should().Be(intersection.Object);
            _precomputationResult.Point.Should().Be(Tuple.Point(0, 0, -1));
            _precomputationResult.EyeVector.Should().Be(Tuple.Vector(0, 0, -1));
            _precomputationResult.NormalVector.Should().Be(Tuple.Vector(0, 0, -1));
        }

        private void ThenThePreCompN2Is(double n2)
        {
            _precomputationResult.N2.Should().Be(n2);
        }

        private void ThenThePreCompN1Is(double n1)
        {
            _precomputationResult.N1.Should().Be(n1);
        }
    }
}
