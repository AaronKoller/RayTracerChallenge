using FluentAssertions;
using RayTracer.Shapes;
using System.Linq;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.ShapesTests.GroupTests
{
    public class GroupTests
    {
        private Group _groupResult;
        private Group _group;
        private Shape _shape;
        private Ray _ray;
        private Intersections _intersections;
        private Shape _shape1;
        private Shape _shape2;
        private Shape _shape3;
        private RayTracer.BoundingBox _boundingBoxResult;

        [Fact]
        public void WhereANewGroupIsCreated()
        {
            //When
            WhenANewGroupIsCreated(new Group());

            //Then
            ThenGroupHasTheFollowingProperties();
        }

        [Fact]
        public void WhereAddingAChildToAGroup_TheChildIsFound()
        {
            //Given
            GivenAGroup(new Group());
            GivenAShape(new TestShape());

            //When
            WhenAShapeIsAddedToAGroup(_group, _shape);

            //Then
            ThenTheGroupIsNotEmpty();
            ThenTheGroupHasTheShape(_shape);
            ThenTheShapesParentIsThatSameGroup(_group);
        }

        [Fact]
        public void WhereIntersectingARayWithAnEmptyGroup_ThenNoIntersectionsAreReturned()
        {
            //Given
            GivenAGroup(new Group());
            GivenARay(new Ray(Tuple.OriginPoint, Tuple.Vector(0, 0, 1)));

            //When
            WhenLocalIntersectIsCalledOnTheGroup(_group, _ray);

            //Then
            ThenIntersectionsAreEmpty(_intersections);
        }

        [Fact]
        public void WhereIntersectingARayWithANonEmptyGroup_ThenSortedIntersectionsAreReturned()
        {
            //Given
            GivenAGroup(new Group());
            GivenAShapeAddedToTheGroup1(_group, new Sphere { Id = 0});
            GivenAShapeAddedToTheGroup2(_group, new Sphere{Id = 1, Transform = Matrix.Transform.Translation(0,0,-3)});
            GivenAShapeAddedToTheGroup3(_group, new Sphere{Id = 2, Transform = Matrix.Transform.Translation(5,0,0)});
            GivenARay(new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1)));

            //When
            WhenLocalIntersectIsCalledOnTheGroup(_group, _ray);

            //Then
            ThenTheIntersectCountIs(4);
            ThenTheIntersectedObjectAtIndexIs(0, _shape2);
            ThenTheIntersectedObjectAtIndexIs(1, _shape2);
            ThenTheIntersectedObjectAtIndexIs(2, _shape1);
            ThenTheIntersectedObjectAtIndexIs(2, _shape1);
        }

        [Fact]
        public void WhereIntersectingATransformedGroup_TheIntersectionsAreReturnedForGroupAndSphereTransformation()
        {
            //Given
            GivenAGroup(new Group());
            GivenAGroupTransform(_group, Matrix.Transform.Scale(2, 2, 2));
            GivenAShapeAddedToTheGroup1(_group, new Sphere{Transform = Matrix.Transform.Translation(5,0,0)});
            GivenARay(new Ray(Tuple.Point(10, 0, -10), Tuple.Vector(0, 0, 1)));

            //When
            WhenIntersectIsCalledOnTheGroup(_group, _ray);

            //Then
            ThenTheIntersectCountIs(2);
        }

        [Fact]
        public void AGroupHasABoundingBoxThatContainsItsChildren()
        {
            //Given
            GivenAGroup(new Group());
            GivenAShapeAddedToTheGroup1(_group, new Sphere
            {
                Transform = Matrix.Transform.Translation(2,5,-3) * Matrix.Transform.Scale(2,2,2)
            });

            GivenAShapeAddedToTheGroup2(_group, new Cylinder
            {
                Minimum = -2,
                Maximum = 2,
                Transform = Matrix.Transform.Translation(-4,-1,4) * Matrix.Transform.Scale(.5,1,.5)
            });

            //When
            WhenBoundsOfIsCalled(_group);

            //Then
            TheBoxMinPointIs(Tuple.Point(-4.5, -3, -5));
            TheBoxMaxPointIs(Tuple.Point(4, 7, 4.5));
        }

        private void TheBoxMaxPointIs(Tuple point)
        {
            _boundingBoxResult.Max.Should().Be(point);
        }

        private void TheBoxMinPointIs(Tuple point)
        {
            _boundingBoxResult.Min.Should().Be(point);
        }

        private void WhenBoundsOfIsCalled(Group group)
        {
            _boundingBoxResult = group.BoundsOf();
        }

        private void WhenIntersectIsCalledOnTheGroup(Group group, Ray ray)
        {
            _intersections = group.Intersect(ray);
        }

        private void WhenLocalIntersectIsCalledOnTheGroup(Group group, Ray ray)
        {
            _intersections = group.LocalIntersect(ray);
        }

        private void GivenAGroupTransform(Group group, Matrix transform)
        {
            group.Transform = transform;
            _group = group;
        }

        private void ThenTheIntersectedObjectAtIndexIs(int i, Shape shape)
        {
            _intersections[i].Object.Should().Be(shape);
        }

        private void GivenAShapeAddedToTheGroup1(Group group, Shape shape)
        {
            _group.AddChild(shape);
            _shape1 = shape;
        }

        private void GivenAShapeAddedToTheGroup2(Group group, Shape shape)
        {
            _group.AddChild(shape);
            _shape2 = shape;
        }
        private void GivenAShapeAddedToTheGroup3(Group group, Shape shape)
        {
            _group.AddChild(shape);
            _shape3 = shape;
        }


        private void ThenTheIntersectCountIs(int i)
        {
            _intersections.Count.Should().Be(i);
        }


        private void ThenIntersectionsAreEmpty(Intersections intersections)
        {
            _intersections.Should().BeNull();
        }



        private void GivenARay(Ray ray)
        {
            _ray = ray;
        }

        private void ThenTheShapesParentIsThatSameGroup(Group group)
        {
            _shape.Parent.Should().Be(group);
        }

        private void ThenTheGroupHasTheShape(Shape shape)
        {
            _group.Should().Contain(x => x == shape);
        }

        private void ThenTheGroupIsNotEmpty()
        {
            _group.Should().NotBeNull();
        }

        private void WhenAShapeIsAddedToAGroup(Group group, Shape shape)
        {
            group.AddChild(shape);
        }

        private void GivenAShape(Shape shape)
        {
            _shape = shape;
        }

        private void GivenAGroup(Group @group)
        {
            _group = group;
        }


        private void ThenGroupHasTheFollowingProperties()
        {
            _groupResult.Transform.Data.Should().BeEquivalentTo(new Matrix().Identity.Data);
            _groupResult.Count().Should().Be(0);
        }

        private void WhenANewGroupIsCreated(Group group)
        {
            _groupResult = group;
        }
    }
}
