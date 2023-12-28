using System;

namespace RayTracer.Shapes
{
    public class Triangle : Shape
    {
        public Tuple Point1 { get; }
        public Tuple Point2 { get; }
        public Tuple Point3 { get; }
        public Tuple Edge1 { get; }
        public Tuple Edge2 { get; }
        public Tuple Normal { get; }

        public Triangle(Tuple point1, Tuple point2, Tuple point3)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
            Edge1 = point2 - point1;
            Edge2 = point3 - point1;
            Normal = Edge2.Cross(Edge1).Normalize();
        }

        public override Tuple LocalNormalAt(Tuple point)
        {
            return Normal;
        }

        public override Intersections LocalIntersect(Ray ray)
        {
            var dirCrossEdge2 = ray.Direction.Cross(Edge2);
            var determinant = Edge1.Dot(dirCrossEdge2);

            if (Math.Abs(determinant) < Constants.EPSILON)
            {
                return null;
            }

            var f = 1.0 / determinant;
            var p1ToOrigin = ray.Origin - Point1;
            var u = f * p1ToOrigin.Dot(dirCrossEdge2);

            if (u < 0 || u > 1)
            {
                return null;
            }

            var originCrossE1 = p1ToOrigin.Cross(Edge1);
            var v = f * ray.Direction.Dot(originCrossE1);

            if (v < 0 || (u + v) > 1)
            {
                return null;
            }

            var t = f * Edge2.Dot(originCrossE1);

            return new Intersections
            {
                [0] = new Intersection(t, this)
            };
        }

        public override BoundingBox BoundsOf()
        {
            throw new System.NotImplementedException();
        }
    }
}

