using RayTracer.Shapes;
using RayTracer.Tests.BoundingBox;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.ShapesTests
{
    public class ParentSpaceBoundsOfTests : _boundingBoxTestsBase
    {
        private Shape _shape;

        [Fact]
        public void QueryingAShapesBoundingBoxInItsParentsSpace()
        {
            //Given
            GivenAShape(new Sphere
            {
                Transform = Matrix.Transform.Translation(1, -3, 5) * Matrix.Transform.Scale(.5, 2, 4)
            });

            //When
            WhenParentSpaceBoundsOfIsCalled(_shape);

            //Then
            ThenTheMinIs(Tuple.Point(.5,-5,1));
            ThenTheMaxIs(Tuple.Point(1.5,-1,9));
        }

        private void WhenParentSpaceBoundsOfIsCalled(Shape shape)
        {
            _boundingBoxResult = shape.ParentSpaceBoundsOf();
        }

        private void GivenAShape(Shape shape)
        {
            _shape = shape;
        }
    }
}
