using System;

namespace RayTracer.Patterns
{
    public class RingPattern : Pattern
    {
        public RingPattern(Color color1, Color color2) : base(color1, color2) { }

        public override Color PatternAt(Tuple point)
        {
            return Math.Abs(Math.Floor(Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Z, 2))) % 2)  < Constants.EPSILON ? A : B;
        }
    }
}