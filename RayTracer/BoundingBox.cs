using System.Collections.Generic;
using RayTracer.Shapes;

namespace RayTracer
{
    public class BoundingBox
    {

        public BoundingBox() : this(
            Tuple.Point(Constants.INFINITY, Constants.INFINITY, Constants.INFINITY),
            Tuple.Point(-Constants.INFINITY, -Constants.INFINITY, -Constants.INFINITY)) { }

        public BoundingBox(Tuple min, Tuple max)
        {
            Min = min;
            Max = max;
        }
        //Yes the signs seem to be wrong, but this just stresses that the bounding box is invalid if it is not set.
        public Tuple Min { get;}
        public Tuple Max { get;}

        public static BoundingBox ParentSpaceBoundsOf(Shape shape)
        {

            return shape.BoundsOf().Transform(shape.Transform);
        }

        public static BoundingBox operator +(BoundingBox boundingBox1, BoundingBox boundingBox2)
        {
            boundingBox1.Resize(boundingBox2.Min);
            boundingBox1.Resize(boundingBox2.Max);

            return boundingBox1;
        } 

        public void Resize(Tuple point)
        {
            Min.X = point.X < Min.X ? point.X : Min.X;
            Min.Y = point.Y < Min.Y ? point.Y : Min.Y;
            Min.Z = point.Z < Min.Z ? point.Z : Min.Z;

            Max.X = point.X > Max.X ? point.X : Max.X;
            Max.Y = point.Y > Max.Y ? point.Y : Max.Y;
            Max.Z = point.Z > Max.Z ? point.Z : Max.Z;
        }

        public bool ThisBoxContains(Tuple point)
        {
            return point.X >= Min.X && point.X <= Max.X &&
                   point.Y >= Min.Y && point.Y <= Max.Y &&
                   point.Z >= Min.Z && point.Z <= Max.Z;


        }

        public bool ThisBoxContains(BoundingBox boundingBox)
        {
            return ThisBoxContains(boundingBox.Min) && ThisBoxContains(boundingBox.Max);
        }

        public BoundingBox Transform(Matrix transform)
        {

            var points = new List<Tuple>();
            points.Add(Min);
            points.Add(Tuple.Point(Min.X, Min.Y, Max.Z));
            points.Add(Tuple.Point(Min.X, Max.Y, Min.Z));
            points.Add(Tuple.Point(Min.X, Max.Y, Max.Z));
            points.Add(Tuple.Point(Max.X, Min.Y, Min.Z));
            points.Add(Tuple.Point(Max.X, Min.Y, Max.Z));
            points.Add(Tuple.Point(Max.X, Max.Y, Min.Z));
            points.Add(Max);

            var newBoundingBox = new BoundingBox();

            foreach (var point in points)
            {
                newBoundingBox.Resize(transform * point);
            }


            //var p1 = Min;
            //var p2 = Tuple.Point(Min.X, Min.Y, Max.Z);
            //var p3 = Tuple.Point(Min.X, Max.Y, Min.Z);
            //var p4 = Tuple.Point(Min.X, Max.Y, Max.Z);
            //var p5 = Tuple.Point(Max.X, Min.Y, Min.Z);
            //var p6 = Tuple.Point(Max.X, Min.Y, Max.Z);
            //var p7 = Tuple.Point(Max.X, Max.Y, Min.Z);
            //var p8 = Max;


            return newBoundingBox;

        }  
    }
}