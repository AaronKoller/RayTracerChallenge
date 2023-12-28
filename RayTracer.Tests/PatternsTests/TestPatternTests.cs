using FluentAssertions;
using RayTracer.Patterns;
using RayTracer.Shapes;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.PatternsTests
{
    public class TestPatternTests : _PatternTestsBase
    {
        private Pattern _resultTestPattern;
        private Matrix _transform;
        private Shape _shape;
        private Color _resultColor;

        [Fact]
        public void WhereATestPatternIsCreated_TheDefaultTransformationIsReturned()
        {
            //Given

            //When
            WhenATestPatternIsCreated(new TestPattern());

            //Then
            ThenTheTransformIsReturned(new Matrix().GenerateIdentityMatrix());
        }

        [Fact]
        public void WhereATransformIsSet_ThenTheTransformIsReturned()
        {
            //Given
            GivenATransform(Matrix.Transform.Translation(1, 2, 3));
            GivenAPattern(new TestPattern());

            //When
            WhenTheTransformIsSet(_pattern, _transform);

            //Then
            ThenTheTransformIsReturned(_transform);
        }

        [Fact]
        public void WhereAPatternWithAnObjectTransformation_ThenAColorIsReturned()
        {
            //Given
            GivenAShape(new Sphere());
            GivenATransformOnAShape(_shape, Matrix.Transform.Scale(2, 2, 2));
            GivenAPattern(new TestPattern());
            
            //When
            WhenPatternAtObjectIsCalled(_pattern,_shape, Tuple.Point(2,3,4));

            //Then
            ThenTheColorIs(new Color(1, 1.5, 2));
        }

        [Fact]
        public void WhereAPatternWithAnPatternTransformation_ThenAColorIsReturned()
        {
            //Given
            GivenAShape(new Sphere());
            GivenAPattern(new TestPattern());
            GivenATransformOnAPattern(_pattern, Matrix.Transform.Scale(2, 2, 2));

            //When
            WhenPatternAtObjectIsCalled(_pattern, _shape, Tuple.Point(2, 3, 4));

            //Then
            ThenTheColorIs(new Color(1, 1.5, 2));
        }

        [Fact]
        public void WhereAPatternWithAnPatternAndShapteTransformation_ThenAColorIsReturned()
        {
            //Given
            GivenAShape(new Sphere());
            GivenATransformOnAShape(_shape, Matrix.Transform.Scale(2, 2, 2));
            GivenAPattern(new TestPattern());
            GivenATransformOnAPattern(_pattern, Matrix.Transform.Translation(.5,1,1.5));

            //When
            WhenPatternAtObjectIsCalled(_pattern, _shape, Tuple.Point(2.5,3,3.5));

            //Then
            ThenTheColorIs(new Color(.75,.5,.25));
        }

        private void GivenATransformOnAPattern(Pattern pattern, Matrix transform)
        {
            pattern.Transform = transform;
        }

        private void ThenTheColorIs(Color color)
        {
            _resultColor.Should().BeEquivalentTo(color);
        }

        private void WhenPatternAtObjectIsCalled(Pattern pattern, Shape shape, Tuple point)
        {
            _resultColor = pattern.PatternAtObject(shape, point);
        }

        private void GivenATransformOnAShape(Shape shape, Matrix transform)
        {
            shape.Transform = transform;
        }

        private void GivenAShape(Shape shape)
        {
            _shape = shape;
        }

        private void WhenTheTransformIsSet(Pattern pattern, Matrix transform)
        {
            pattern.Transform = transform;
            _resultTestPattern = pattern;
        }

        private void GivenATransform(Matrix transform)
        {
            _transform = transform;
        }

        private void ThenTheTransformIsReturned(Matrix identityMatrix)
        {
            _resultTestPattern.Transform.Data.Should().BeEquivalentTo(identityMatrix.Data);
        }

        private void WhenATestPatternIsCreated(TestPattern testPattern)
        {
            _resultTestPattern = testPattern;
        }
    }
}
