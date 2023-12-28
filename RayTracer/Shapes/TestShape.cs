namespace RayTracer.Shapes
{
    public class TestShape : Shape
    {
        public override Intersections LocalIntersect(Ray ray)
        {
            return new Intersections { SavedRay = ray }; ;
        }

        public override BoundingBox BoundsOf()
        {
            return new BoundingBox(Tuple.Point(-1, -1, -1), Tuple.Point(1, 1, 1));
        }

        public override Tuple LocalNormalAt(Tuple tuple)
        {
            return Tuple.Vector(tuple.X, tuple.Y, tuple.Z);
        }

    }
}