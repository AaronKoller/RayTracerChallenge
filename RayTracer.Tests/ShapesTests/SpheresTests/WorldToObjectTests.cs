using FluentAssertions;
using RayTracer.Shapes;
using RayTracer.Transformations;
using System;
using Xunit;

namespace RayTracer.Tests.ShapesTests.SpheresTests
{
    public class WorldToObjectTests : _SphereTestsBase
    {
        private Group _group1;
        private Group _group2;
        private Shape _shape;
        private Tuple _pointResult;

        [Fact]
        public void WhereConvertingAPointFromWorldToObjectSpace_ThenAPointIsReturned()
        {
            //Given
            GivenAGroupWithATransformation1(new Group { Transform = Matrix.Transform.Rotation_y(Math.PI / 2) });
            GivenAGroupWithATransformation2(new Group { Transform = Matrix.Transform.Scale(2, 2, 2) });
            GivenChildGroupAddedToGroup(_group1, _group2);
            GivenAShape(new Sphere { Transform = Matrix.Transform.Translation(5, 0, 0) });
            GivenChildShapeAddedToAGroup(_group2, _shape);

            //When
            WhenWorldToObjectIsCalled(_shape, Tuple.Point(-2, 0, -10));

            //Then
            ThenTheResultPointIs(Tuple.Point(0, 0, -1));
        }

        private void ThenTheResultPointIs(Tuple point)
        {
            _pointResult.Should().BeEquivalentTo(point);
        }

        private void WhenWorldToObjectIsCalled(Shape shape, Tuple point)
        {
            _pointResult = shape.WorldToObject(point);
        }

        private void GivenChildShapeAddedToAGroup(Group group2, Shape shape)
        {
            _group2.AddChild(shape);
        }

        private void GivenAShape(Shape shape)
        {
            _shape = shape;
        }

        private void GivenChildGroupAddedToGroup(Group group1, Group group2)
        {
            group1.AddChild(group2);
        }

        private void GivenAGroupWithATransformation1(Group @group)
        {
            _group1 = group;
        }

        private void GivenAGroupWithATransformation2(Group @group)
        {
            _group2 = group;
        }
    }
}
