using FluentAssertions;
using RayTracer.Patterns;

namespace RayTracer.Tests.PatternsTests
{
    public class _PatternTestsBase
    {
        internal Pattern _pattern;
        internal Color _resultColor;

        internal void GivenAPattern(Pattern pattern)
        {
            _pattern = pattern;
        }

        internal void WhenPatternAtIsCalled(Tuple point)
        {
            _resultColor = _pattern.PatternAt(point);
        }

        internal void ThenTheColorIs(Color color)
        {
            _resultColor.Should().BeEquivalentTo(color);
        }
    }
}
