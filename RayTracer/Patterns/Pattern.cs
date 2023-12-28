using System.Data.Common;
using RayTracer.Shapes;

namespace RayTracer.Patterns
{
    public abstract class Pattern
    {

        public Matrix Transform { get; set; }
        public Color A { get; set; }
        public Color B { get; set; }
        public Pattern(Color color1, Color color2)
        {
            A = color1;
            B = color2;
            Transform = new Matrix().GenerateIdentityMatrix();
        }

        public abstract Color PatternAt(Tuple point);

        public Color PatternAtObject(Shape shape, Tuple worldPoint)
        {
            Tuple objectPoint = shape.WorldToObject(worldPoint);
            //Tuple objectPoint = shape.Transform.Inverse() * worldPoint;
            Tuple patternPoint = Transform.Inverse() * objectPoint;
            Color color = PatternAt(patternPoint);
            return color;
        }

        public Color Blended(Pattern pattern1, Shape shape, Tuple worldPoint)
        {
            var color1 = PatternAtObject(shape, worldPoint);
            var color2 = pattern1.PatternAtObject(shape, worldPoint);

            return color1 + color2;
        }
    }
}