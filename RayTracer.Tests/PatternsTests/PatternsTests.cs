using FluentAssertions;
using RayTracer.Patterns;
using RayTracer.Shapes;
using RayTracer.Transformations;
using Xunit;

namespace RayTracer.Tests.PatternsTests
{
    public class PatternsTests
    {

        private readonly Color _black = Color.Black;
        private readonly Color _white = Color.White;
        private StripePattern _resultStripePattern;
        private StripePattern _stripePattern;
        private Color _colorResult;
        private Shape _shape;


        [Fact]
        public void CreateStripePattern()
        {
            //Given


            //When
            WhenAStripePatternIsCreated(new StripePattern(_white, _black));

            //Then
            ThenTheFollowingPropertiesAreValid();

        }

        public static TheoryData<double, double, double, Color> memberData()
        {
            return new TheoryData<double, double, double, Color>
            {
                {   0, 0, 0, Color.White},
                {   0, 1, 0, Color.White},
                {   0, 2, 0, Color.White},
                {   0, 0, 0, Color.White},
                {   0, 0, 1, Color.White},
                {   0, 0, 2, Color.White},
                {  .9, 0, 0, Color.White},
                {   1, 0, 0, Color.Black},
                { -.1, 0, 0, Color.Black},
                {  -1, 0, 0, Color.Black},
                {-1.1, 0, 0, Color.White},
            };
        }

        [Theory]
        [MemberData(nameof(memberData))]

        public void WhereStripeAtPointIsCalled_AColorIsReturned(double x, double y, double z, Color color)
        {
            //Given
            GivenAStripePattern(new StripePattern(_white, _black));

            //When
            WhenStripeAtIsCalled(Tuple.Point(x, y, z));

            //Then
            ThenTheColorIs(color);
        }

        [Fact]
        public void StripesWithAnObjectTransformation()
        {
            //Given
            GivenAShape(new Sphere());
            GivenATransformOnAShape(_shape, Matrix.Transform.Scale(2, 2, 2));
            GivenAStripePattern(new StripePattern(Color.White, Color.Black ));

            //When
            WhenStripeAtObjectIsCalled(_stripePattern, _shape, Tuple.Point(1.5, 0, 0));

            //Then
            ThenTheColorIs(Color.White);
        }

        [Fact]
        public void StripesWithAPatternTransformation()
        {
            //Given
            GivenAShape(new Sphere());
            GivenAStripePattern(new StripePattern(Color.White, Color.Black));
            GivenATransformationIsAppliedToThePattern(_stripePattern, Matrix.Transform.Scale(2, 2, 2));

            //When
            WhenStripeAtObjectIsCalled(_stripePattern, _shape, Tuple.Point(1.5, 0, 0));

            //Then
            ThenTheColorIs(Color.White);

        }

        [Fact]
        public void StripesWithAPatternAndObjectTransformation()
        {
            //Given
            GivenAShape(new Sphere());
            GivenATransformOnAShape(_shape, Matrix.Transform.Scale(2, 2, 2));
            GivenAStripePattern(new StripePattern(Color.White, Color.Black));
            GivenATransformationIsAppliedToThePattern(_stripePattern, Matrix.Transform.Translation(.5, 0,0));

            //When
            WhenStripeAtObjectIsCalled(_stripePattern, _shape, Tuple.Point(2.5, 0, 0));

            //Then
            ThenTheColorIs(Color.White);

        }

        private void GivenATransformationIsAppliedToThePattern(StripePattern stripePattern, Matrix transform)
        {
            stripePattern.Transform = transform;
        }

        private void WhenStripeAtObjectIsCalled(StripePattern stripePattern, Shape shape, Tuple point)
        {
            _colorResult = stripePattern.PatternAtObject(shape, point);
        }

        private void GivenATransformOnAShape(Shape shape, Matrix transform)
        {
            shape.Transform = transform;
        }

        private void GivenAShape(Shape shape)
        {
            _shape = shape;
        }

        private void GivenAStripePattern(StripePattern stripePattern)
        {
            _stripePattern = stripePattern;
        }

        private void WhenStripeAtIsCalled(Tuple point)
        {
            _colorResult = _stripePattern.PatternAt(point);
        }

        private void WhenAStripePatternIsCreated(StripePattern stripePatternPattern)
        {
            _resultStripePattern = stripePatternPattern;
        }

        private void ThenTheColorIs(Color color)
        {
            _colorResult.Should().BeEquivalentTo(color);
        }

        private void ThenTheFollowingPropertiesAreValid()
        {
            _resultStripePattern.A.Should().Be(_white);
            _resultStripePattern.B.Should().Be(_black);
        }
    }
}
