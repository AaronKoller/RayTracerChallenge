namespace RayTracer
{
    public class Ray
    {
        private readonly Tuple _origin;
        private readonly Tuple _direction;
        public Tuple Origin => _origin;
        public Tuple Direction => _direction;


        //This is the Transform(ray, matrix) method in the book
        public static Ray operator *(Matrix matrix, Ray ray)
        {
            var newOrigin = matrix * ray.Origin;
            var newDirection = matrix * ray.Direction;
            return new Ray(newOrigin, newDirection);
        }

        public Ray(Tuple origin, Tuple direction)
        {
            _origin = origin;
            _direction = direction;
        }

        public Tuple Position(double time)
        {
            return _origin + _direction * time;
        }
    }
}