using System;

namespace RayTracer.Shapes
{
    public class Plane : Shape
    {
        public override Tuple LocalNormalAt(Tuple point)
        {
            //The plane returns the same normal everywhere regardless of the point.
            return Tuple.Vector(0,1,0);
        }

        public override Intersections LocalIntersect(Ray ray)
        {
            if (Math.Abs(ray.Direction.Y) < Constants.EPSILON) return null;

            var t = -ray.Origin.Y / ray.Direction.Y;
            return new Intersections {[0] = new Intersection(t, this)};
        }

        public override BoundingBox BoundsOf()
        {
            return new BoundingBox(
                Tuple.Point(-Constants.INFINITY, 0, -Constants.INFINITY),
                Tuple.Point(Constants.INFINITY, 0, Constants.INFINITY));
        }
    }
}