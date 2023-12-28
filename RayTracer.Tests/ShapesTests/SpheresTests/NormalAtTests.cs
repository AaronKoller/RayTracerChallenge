using FluentAssertions;
using RayTracer.Shapes;
using System;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.ShapesTests.SpheresTests
{
    public class NormalAtTests : _SphereTestsBase
    {
        private Tuple _point;
        private Tuple _resultVector;
        private double _number;


        public static TheoryData<double, double, double, double, double, double> memberData()
        {
            double number = Math.Sqrt(3) / 3;
            return new TheoryData<double, double, double, double, double, double>
            {
                {1, 0, 0, 1, 0, 0},
                {0, 1, 0, 0, 1, 0},
                {-0, 0, 1, 0, 0, 1},
                {number,number,number,number,number,number},
            };
        }

        [Theory]
        [MemberData(nameof(memberData))]
        public void WhereAPointOnTheSurfaceOfASphere_TheNormalVectorIsComputed(double x, double y, double z, double vX, double vY, double vZ)
        {
            //Given
            GivenASphere(new Sphere());
            GivenAPoint(Tuple.Point(x, y, z));

            //When
            WhenNormalAtReturnsResultVector(_sphere, _point);

            //Then
            ThenTheResultVectorIsReturned(Tuple.Vector(vX, vY, vZ));
        }

        [Fact]
        public void WhereAPointOnTheSurfaceOfATranslatedSphere_TheNormalVectorIsComputed()
        {
            //Given
            GivenASphere(new Sphere());
            GivenAPoint(Tuple.Point(0, 1.70711, -0.70711));
            GivenATransformation(Matrix.Transform.Translation(0,1,0));
            GivenTheTransformationIsSetOnASphere(_transformMatrix);

            //When
            WhenNormalAtReturnsResultVector(_sphere, _point);

            //Then
            ThenTheResultVectorIsReturned(Tuple.Vector(0, 0.70711, -0.70711));
        }

        [Fact]
        public void WhereAPointOnTheSurfaceOfAScalingRotatedSphere_TheNormalVectorIsComputed()
        {
            //Given
            GivenASphere(new Sphere());
            GivenATransformation(Matrix.Transform.Scale(1, .5, 1) * Matrix.Transform.Rotation_z(Math.PI / 5));
            GivenAPoint(Tuple.Point(0,Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2));
            GivenTheTransformationIsSetOnASphere(_transformMatrix);

            //When
            WhenNormalAtReturnsResultVector(_sphere, _point);

            //Then
            ThenTheResultVectorIsReturned(Tuple.Vector(0, 0.97014, -.24254));
        }

        private void GivenATransformation(Matrix transformation)
        {
            _transformMatrix = transformation;
        }

        private void WhenNormalAtReturnsResultVector(Sphere sphere, Tuple point)
        {
            _resultVector = _sphere.NormalAt(point);
        }

        private void ThenTheResultVectorIsReturned(Tuple vector)
        {
            _resultVector.Should().Be(vector);
        }

        private void GivenAPoint(Tuple point)
        {
            _point = point;
        }
    }
}
