using System;

namespace RayTracer.Patterns
{
    public class GradientPattern : Pattern
    {
        public GradientPattern(Color color1, Color color2) : base(color1, color2){ }

        public override Color PatternAt(Tuple point)
        {
            var distance = B - A;
            var fraction = point.X - Math.Floor(point.X);

            var color = A + distance * fraction;
            return color;
        }
    }
}