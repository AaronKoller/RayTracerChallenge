using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using RayTracer.Shapes;
using RayTracer.Tests.RayTest;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.ShapesTests.TestShapeTests
{
    public class TestShapeTests
    {
        private TestShape _resultTestShape;
        private TestShape _testShape;
        private Matrix _transform;
        private Material _material;
        private Ray _ray;
        private Intersections _intersections;
        private Tuple _resultVector;
        private Group _group1;


        [Fact]
        public void WhereADefaultTestShapeIsCreated_TheIdentityMatrixReturned()
        {

            //When
            WhenATestShapeIsCreated(new TestShape());

            //Then
            ThenTheShapeHasTheFollowingPropeties();
        }

        private void ThenTheShapeHasTheFollowingPropeties()
        {
            _resultTestShape.Transform.Data.Should().BeEquivalentTo(new Matrix().GenerateIdentityMatrix().Data);
            _resultTestShape.Parent.Should().BeNull();
        }

        [Fact]
        public void WhereATransformIsSetOnATestShape_TheTransformIsReturned()
        {
            //Given
            GivenATestShape(new TestShape());
            GivenATransformation(Matrix.Transform.Translation(2, 3, 4));

            //When
            WhenATransformationIsSetOnShape(_testShape, _transform);


            //Then
            ThenTheTransformIsReturned(_transform);
        }

        [Fact]
        public void WhereATestShapeIsCreated_TheDefaultMaterialIsReturned()
        {
            //When
            WhenATestShapeIsCreated(new TestShape());

            //Then
            ThenTheMaterialIsReturned(new Material());
        }

        [Fact]
        public void WhereATestShapeMaterialIsSet_TheMaterialShouldBeReturned()
        {
            //Given
            GivenATestShape(new TestShape());
            GivenAMaterial(new Material());
            GivenAnAmbiance(1);

            //When
            WhenTheMaterialIsSetOnTheShape(_testShape, _material);

            //Then
            ThenTheMaterialIsReturned(_material);
        }

        [Fact]
        public void IntersectionAScaledShapeWithARay()
        {
            //Given
            GivenARay(new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1)));
            GivenATestShape(new TestShape());
            GivenATransformation(Matrix.Transform.Scale(2,2,2));
            //When
            WhenATransformationIsSetOnShape(_testShape, _transform);
            WhenIntersectIsCalled(_testShape, _ray);

            //Then
            ThenTheIntersectionsSavedRayOriginIs(Tuple.Point(0,0,-2.5));
            ThenTheIntersectionsSavedRayVectorIs(Tuple.Vector(0,0,.5));
        }

        [Fact]
        public void IntersectionATranslatedShapeWithARay()
        {
            //Given
            GivenARay(new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1)));
            GivenATestShape(new TestShape());
            GivenATransformation(Matrix.Transform.Translation(5,0,0));
            //When
            WhenATransformationIsSetOnShape(_testShape, _transform);
            WhenIntersectIsCalled(_testShape, _ray);

            //Then
            ThenTheIntersectionsSavedRayOriginIs(Tuple.Point(-5, 0, -5));
            ThenTheIntersectionsSavedRayVectorIs(Tuple.Vector(0, 0, 1));
        }

        [Fact]
        public void ComputingTheNormalOnATranslatedShape()
        {
            //Given
            GivenATestShape(new TestShape());
            GivenATransformation(Matrix.Transform.Translation(0,1,0));

            //When
            WhenATransformationIsSetOnShape(_testShape, _transform);
            WhenNormalAtIsCalledAtPoint(Tuple.Point(0,1.70711, -0.70711));

            //Then
            ThenVectorIsReturned(Tuple.Vector(0, 0.70711, -0.70711));
        }

        [Fact]
        public void ComputingTheNormalOnATransformedShape()
        {
            //Given
            GivenATestShape(new TestShape());
            GivenATransformation(Matrix.Transform.Scale(1,.5,1) * Matrix.Transform.Rotation_z(Math.PI/5));

            //When
            WhenATransformationIsSetOnShape(_testShape, _transform);
            WhenNormalAtIsCalledAtPoint(Tuple.Point(0, Math.Sqrt(2)/2, -Math.Sqrt(2) / 2));

            //Then
            ThenVectorIsReturned(Tuple.Vector(0, 0.97014, -.24254));
        }





        private void ThenVectorIsReturned(Tuple vector)
        {
            _resultVector.Should().Be(vector);
        }

        private void WhenNormalAtIsCalledAtPoint(Tuple point)
        {
            _resultVector = _testShape.NormalAt(point);
        }

        private void GivenARay(Ray ray)
        {
            _ray = ray;
        }

        private void GivenAnAmbiance(int i)
        {
            _material.Ambient = i;
        }

        private void GivenAMaterial(Material material)
        {
            _material = material;
        }

        private void GivenATransformation(Matrix transformation)
        {
            _transform = transformation;
        }

        private void GivenATestShape(TestShape testShape)
        {
            _testShape = testShape;
        }

        private void WhenATestShapeIsCreated(TestShape testShape)
        {
            _resultTestShape = testShape;
        }

        private void WhenIntersectIsCalled(TestShape testShape, Ray ray)
        {
            _intersections = testShape.Intersect(ray);
        }

        private void WhenTheMaterialIsSetOnTheShape(TestShape testShape, Material material)
        {
            _testShape.Material = material;
            _resultTestShape = _testShape;
        }

        private void WhenATransformationIsSetOnShape(TestShape testShape, Matrix transform)
        {
            _resultTestShape = _testShape;
            _resultTestShape.Transform = transform;
        }

        private void ThenTheIntersectionsSavedRayVectorIs(Tuple vector)
        {
            _intersections.SavedRay.Direction.Should().Be(vector);
        }

        private void ThenTheIntersectionsSavedRayOriginIs(Tuple point)
        {
            _intersections.SavedRay.Origin.Should().Be(point);
        }

        private void ThenTheMaterialIsReturned(Material material)
        {
            _resultTestShape.Material.Should().BeEquivalentTo(material);
        }

        private void ThenTheTransformIsReturned(Matrix transform)
        {
            _resultTestShape.Transform.Should().Be(transform);
        }
    }
}
