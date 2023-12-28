using System;

namespace RayTracer.Shapes
{
    public class Cone : Shape
    {

        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public bool Closed { get; set; }

        public Cone()
        {
            Minimum = double.MinValue;
            Maximum = double.MaxValue;
        }

        public override Tuple LocalNormalAt(Tuple point)
        {

            var y = Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Z, 2));
            if (point.Y > 0) y = -y;

            var distance = Math.Pow(point.X, 2) + Math.Pow(point.Z, 2);

            if (distance < 1 && point.Y >= Maximum - Constants.EPSILON)
                return Tuple.Vector(0,1,0);
            if (distance < 1 && point.Y <= Minimum + Constants.EPSILON)
                return Tuple.Vector(0, -1, 0);

            var normal = Tuple.Vector(point.X, y, point.Z);
            return normal;
        }

        public override Intersections LocalIntersect(Ray ray)
        {
            Intersections intersections = new Intersections();

            intersections = IntersectCaps(ray, intersections);

            double a = Math.Pow(ray.Direction.X, 2) - 
                       Math.Pow(ray.Direction.Y, 2) + 
                       Math.Pow(ray.Direction.Z, 2);
            double b = 2 * ray.Origin.X * ray.Direction.X -
                       2 * ray.Origin.Y * ray.Direction.Y +
                       2 * ray.Origin.Z * ray.Direction.Z;
            double c = Math.Pow(ray.Origin.X, 2) - 
                       Math.Pow(ray.Origin.Y, 2) + 
                       Math.Pow(ray.Origin.Z, 2);

            if (Math.Abs(a) <= Constants.EPSILON && Math.Abs(b) >= Constants.EPSILON)
            {
                var t = -c / 2 * b;
                intersections.Add(new Intersection(t, this));
            }
            //if a is nonzero you'll use the same algorithm as cylinders
            //therefore if a is almost 0 then exit as it is parallel
            if (Math.Abs(a) <= Constants.EPSILON)
                return intersections;



            double discriminant = Math.Pow(b, 2) - 4 * a * c;
            if (discriminant < 0)
            {
                return intersections;
            }
            double t0 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);

            //swap values
            if (t0 > t1)
            {
                var t1Before = t1;
                t1 = t0;
                t0 = t1Before;
            }

            var y0 = ray.Origin.Y + t0 * ray.Direction.Y;
            if (this.Minimum < y0 && y0 < this.Maximum)
            {
                intersections.Add(new Intersection(t0, this));
            }

            var y1 = ray.Origin.Y + t1 * ray.Direction.Y;
            if (this.Minimum < y1 && y1 < this.Maximum)
            {
                intersections.Add(new Intersection(t1, this));
            }
            return intersections;
        }

        public override BoundingBox BoundsOf()
        {
            var absMinimum = Math.Abs(Minimum);
            var absMaximum = Math.Abs(Maximum);
            var limit = Math.Max(absMaximum, absMinimum);

            return new BoundingBox(
                Tuple.Point(-limit, Minimum, -limit),
                Tuple.Point(limit, Maximum, limit));
        }

        private Intersections IntersectCaps(Ray ray, Intersections intersections)
        {
            //caps only matter if the cylinder is closed, and might possible be intersected by the ray
            if (!Closed || Math.Abs(ray.Direction.Y) <= Constants.EPSILON) return intersections;
            var t = 0.0;

            //check for an intersection with the lower end cap by intersecting
            //the ray with the plane at the y=cly.minimum
            t = (Minimum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, t, Minimum)) intersections.Add(new Intersection(t, this));


            t = (Maximum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, t, Maximum)) intersections.Add(new Intersection(t, this));

            return intersections;
        }

        private bool CheckCap(Ray ray, double t, double y)
        {
            //of 1 (the radius of your cylinders) from the y axis
            var x = ray.Origin.X + t * ray.Direction.X;
            var z = ray.Origin.Z + t * ray.Direction.Z;

            var radius = (Math.Pow(x, 2) + Math.Pow(z, 2));
            var isIntersected = radius <= Math.Pow(y,2);
            return isIntersected;
        }
    }
}