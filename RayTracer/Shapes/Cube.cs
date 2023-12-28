using System;
using System.Linq;

namespace RayTracer.Shapes
{
    public class Cube : Shape
    {
        public override Tuple LocalNormalAt(Tuple point)
        {
            var absX = Math.Abs(point.X);
            var absY = Math.Abs(point.Y);
            var absZ = Math.Abs(point.Z);

            var maxc = new[] {absX, absY, absZ }.Max();
            if (Math.Abs(maxc - absX) < Constants.EPSILON) return Tuple.Vector(point.X, 0, 0);
            if (Math.Abs(maxc - absY) < Constants.EPSILON) return Tuple.Vector(0, point.Y, 0);
            return Tuple.Vector(0, 0, point.Z);
        }

        public override Intersections LocalIntersect(Ray ray)
        {
            (double tMin, double tMax) resultX = CheckAxis(ray.Origin.X, ray.Direction.X);
            (double tMin, double tMax) resultY = CheckAxis(ray.Origin.Y, ray.Direction.Y);
            (double tMin, double tMax) resultZ = CheckAxis(ray.Origin.Z, ray.Direction.Z);

            double tMin = new[] { resultX.tMin, resultY.tMin, resultZ.tMin }.Max();
            double tMax = new[] { resultX.tMax, resultY.tMax, resultZ.tMax }.Min();
            
            //The ray missed
            if (tMin > tMax)
            {
                return new Intersections();
            }

            //The ray intersected 2 points
            Intersections intersections = new Intersections
            {
                [0] = new Intersection(tMin, this),
                [1] = new Intersection(tMax, this),
            };
            return intersections;
        }

        public override BoundingBox BoundsOf()
        {
            return new BoundingBox(Tuple.Point(-1, -1, -1), Tuple.Point(1, 1, 1));
        }


        //The tuple return of tMin and tMax were swapped at one point that created a hard to find subtle bug. Refactor this into an internal class.
        private (double tMin, double tMax) CheckAxis(double origin, double direction)
        {
            double tMin;
            double tMax;

            //1 defines the size of our unit box which has sides of length 1
            double tMinNumerator = (-1 - origin);
            double tMaxNumerator = (1 - origin);


            //as we cannot divide by zero we can set the number to INFINITY (double.maxValue) for those that are less than Epsilon or effectively 0.
            if (Math.Abs(direction) >= Constants.EPSILON)
            {
                tMin = tMinNumerator / direction;
                tMax = tMaxNumerator / direction;
            }
            else
            {
                tMin = Math.Sign(tMinNumerator) * double.MaxValue;
                tMax = Math.Sign(tMaxNumerator) * double.MaxValue;
            }

            if (tMin > tMax)
            {
                return (tMax, tMin);
            }

            return (tMin, tMax);
        }
    }
}