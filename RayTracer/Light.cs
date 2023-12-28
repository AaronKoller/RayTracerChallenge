namespace RayTracer
{
    public class Light
    {
        public Tuple Position { get; }
        public Color Intensity { get; }

        public Light(Tuple position, Color intensity)
        {
            Position = position;
            Intensity = intensity;
        }
    }
}