namespace RayTracer.Patterns
{
    public class TestPattern : Pattern
    {
        public TestPattern() : base(Color.White, Color.White) { }   //White is set here to satisfy the base only and has no real meaning
        public override Color PatternAt(Tuple point)
        {
            return new Color(point.X, point.Y, point.Z);
        }
    }
}