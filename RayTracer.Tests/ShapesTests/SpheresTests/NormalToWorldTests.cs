using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using RayTracer.Shapes;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.ShapesTests.SpheresTests
{
    public class NormalToWorldTests
    {
        private Group _group1;
        private Group _group2;
        private Shape _shape;
        private Tuple _normal;

        [Fact]
        public void ConvertingANormalFromObjectToWorldSpace_Then()
        {

            var number = Math.Sqrt(3)/3;
            //Given
            GivenAGroup1(new Group{Transform = Matrix.Transform.Rotation_y(Math.PI/2)});
            GivenAGroup2(new Group{Transform = Matrix.Transform.Scale(1,2,3)});
            GivenGroupIsAChildOfGroup(_group1, _group2);
            GivenAShape(new Sphere {Transform = Matrix.Transform.Translation(5, 0, 0)});
            GivenAShapeIsAChildOfGroup(_group2, _shape);

            //When
            WhenNormalToWorldIsCalled(_shape, Tuple.Vector(number, number, number));

            //Then

            //TODO why isn't precision working here.  Need to restart?
            ThenTheVectorIsReturned(.0001, Tuple.Vector(.2857, .4286,-.8571));
        }

        [Fact]
        public void WhereFindingTheNormalOnAnObjectInAGroup()
        {
            //Given
            GivenAGroup1(new Group{Transform = Matrix.Transform.Rotation_y(Math.PI/2)});
            GivenAGroup2(new Group{Transform = Matrix.Transform.Scale(1,2,3)});
            GivenGroupIsAChildOfGroup(_group1, _group2);
            GivenAShape(new Sphere { Transform = Matrix.Transform.Translation(5, 0, 0) });
            GivenAShapeIsAChildOfGroup(_group2, _shape);

            //When
            WhenNormalAtIsCalled(_shape, Tuple.Point(1.7321, 1.1547, -5.5774));

            //Then
            ThenTheVectorIsReturned(.01, Tuple.Vector(.2857, .4286, -.8571));
        }

        private void WhenNormalAtIsCalled(Shape shape, Tuple vector)
        {
            _normal = shape.NormalAt(vector);
        }

        private void ThenTheVectorIsReturned(double precision, Tuple vector)
        {
            _normal.X.Should().BeApproximately(vector.X, precision);
            _normal.Y.Should().BeApproximately(vector.Y, precision);
            _normal.Z.Should().BeApproximately(vector.Z, precision);

            //    _normal.Should().BeEquivalentTo(vector, option => option
            //        .Using<double>(ctx => ctx.Subject.Should(). BeApproximately(ctx.Expectation, precision))
            //        .When(info => info.SelectedMemberPath == "a" ||
            //                      info.SelectedMemberPath == "Y" ||
            //                      info.SelectedMemberPath == "Z"));


            //_normal.Should().BeEquivalentTo(vector, option =>
            //    option.Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, precision))
            //        .WhenTypeIs<double>());
        }

            private void WhenNormalToWorldIsCalled(Shape shape, Tuple vector)
        {
            _normal = shape.NormalToWorld(vector);
        }


        private void GivenAShapeIsAChildOfGroup(Group group2, Shape shape)
        {
            _group2.AddChild(shape);
        }

        private void GivenGroupIsAChildOfGroup(Group group1, Group group2)
        {
            group1.AddChild(group2);
        }

        private void GivenAShape(Shape shape)
        {
            _shape = shape;
        }

        private void GivenAGroup2(Group @group)
        {
            _group2 = group;
        }

        private void GivenAGroup1(Group @group)
        {
            _group1 = group;
        }
    }
}
