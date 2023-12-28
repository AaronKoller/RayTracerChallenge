namespace RayTracer.Shapes
{
    public abstract class Shape
    {
        protected Shape()
        {
            Transform = new Matrix().GenerateIdentityMatrix();
            Material = new Material();
        }
        public Shape Parent { get; set; }
        public int Id { get; set; }

        public Matrix Transform { get; set; }
        public Material Material { get; set; }

        public Intersections Intersect(Ray ray)
        {
            Ray localRay = Transform.Inverse() * ray;
            return LocalIntersect(localRay);
        }

        public BoundingBox ParentSpaceBoundsOf()
        {
            return BoundsOf().Transform(Transform);
        }

        public Tuple NormalAt(Tuple worldPoint)
        {

            var localPoint = WorldToObject(worldPoint);
            var localNormal = LocalNormalAt(localPoint);
            var normal = NormalToWorld(localNormal);
            //convert the object from world space to object space (0,0,0) is at the object
            //Tuple localPoint = Transform.Inverse() * worldPoint;


            ////Get the normalized vector in object space
            //Tuple localNormal = LocalNormalAt(localPoint);

            ////convert the object normal from object space back to world space
            //Tuple worldNormal = Transform.Inverse().Transpose() * localNormal;
            //worldNormal.W = 0;

            ////normalize it the previous transform may have ruined our initial normalization
            //Tuple normalizedWorld = worldNormal.Normalize();
            return normal;
        }

        public abstract Tuple LocalNormalAt(Tuple point);

        public abstract Intersections LocalIntersect(Ray ray);

        public abstract BoundingBox BoundsOf();


        public Tuple WorldToObject(Tuple point)
        {
            if (this.Parent != null)
                point = this.Parent.WorldToObject(point);

            var invertedPoint = Transform.Inverse() * point;

            return invertedPoint;
        }

        public Tuple NormalToWorld(Tuple normal)
        {
            normal = this.Transform.Inverse().Transpose() * normal;
            normal.W = 0;
            normal = normal.Normalize();

            if (this.Parent != null)
            {
                normal = this.Parent.NormalToWorld(normal);
            }

            return normal;
        }

    }
}
