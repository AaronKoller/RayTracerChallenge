using System;

namespace RayTracer.Shapes
{
    public class Sphere : Shape
    {
        public override Intersections LocalIntersect(Ray ray)
        {
            Tuple sphereToRay = ray.Origin - Tuple.Point(0, 0, 0);
            double a = ray.Direction.Dot(ray.Direction);
            double b = 2 * ray.Direction.Dot(sphereToRay);
            double c = sphereToRay.Dot(sphereToRay) - 1;

            double discriminant = Math.Pow(b, 2) - 4 * a * c;

            Intersections intersections = new Intersections();

            if (discriminant < 0)
            {
                return intersections;
            }
            double t0 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);

            intersections[0] = new Intersection(t0, this);
            intersections[1] = new Intersection(t1, this);

            return intersections;
        }

        public override BoundingBox BoundsOf()
        {
            return new BoundingBox(Tuple.Point(-1, -1, -1), Tuple.Point(1, 1, 1));
        }


        public override Tuple LocalNormalAt(Tuple localPoint)
        {
            return localPoint - Tuple.OriginPoint;
        }
    }

    public class GlassSphere : Sphere
    {
        public GlassSphere()
        {
            Material = new Material
            {
                Transparency = 1.0,
                RefractiveIndex = 1.5
            };
        }
    }
}
