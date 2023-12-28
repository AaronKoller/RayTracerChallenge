using System;

namespace RayTracer.Patterns
{
    public class StripePattern : Pattern
    {
        public StripePattern(Color color1, Color color2) :  base(color1, color2){ }

        public override Color PatternAt(Tuple point)
        {
            return Math.Abs(Math.Floor(point.X) % 2) < Constants.EPSILON ? A : B;
        }
    }
}