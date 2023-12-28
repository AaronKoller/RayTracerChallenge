using System;

namespace RayTracer.Patterns
{
    public class CheckersPatterns : Pattern
    {
        public CheckersPatterns(Color color1, Color color2) : base(color1, color2)
        {}

        public override Color PatternAt(Tuple point)
        {
            //There is some random noise in this pattern when applied to a plane
            var resultColor = (Math.Floor(point.X) + Math.Floor(point.Y) + Math.Floor(point.Z)) % 2 == 0 ? A : B;
            return resultColor;
        }
    }
}