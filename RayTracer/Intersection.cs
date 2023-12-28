using System;
using System.Collections.Generic;
using System.Linq;
using RayTracer.Shapes;

namespace RayTracer
{
    public class Intersection
    {
       public class PreComputation
        {
            public double T { get; set; }
            public Shape Shape { get; set; }
            public Tuple Point { get; set; }
            public Tuple EyeVector { get; set; }
            public Tuple NormalVector { get; set; }
            public Tuple ReflectVector { get; set; }
            public bool Inside { get; set; }
            public Tuple OverPoint { get; set; }
            public double N2 { get; set; }
            public double N1 { get; set; }
            public Tuple UnderPoint { get; set; }
        }

        private readonly double _t;
        private readonly Shape _shape;

        public Intersection(double t, Shape shape)
        {
            _t = t;
            _shape = shape;
        }

        public double T => _t;
        public Shape Object => _shape;

        public PreComputation PrepareComputations(Ray ray, Intersections intersections = null)
        {
            var preparedOperations = new PreComputation
            {
                T = _t,
                Shape = _shape, 
                Point = ray.Position(_t),
                EyeVector = -ray.Direction,
                Inside = false
            };

            preparedOperations.NormalVector = _shape.NormalAt(preparedOperations.Point);
            preparedOperations.ReflectVector = ray.Direction.Reflect(preparedOperations.NormalVector);
            if (preparedOperations.NormalVector.Dot(preparedOperations.EyeVector) < 0)
            {
                preparedOperations.Inside = true;
                preparedOperations.NormalVector = -preparedOperations.NormalVector;
            }
            preparedOperations.OverPoint = preparedOperations.Point + preparedOperations.NormalVector * Constants.EPSILON;
            preparedOperations.UnderPoint = preparedOperations.Point - preparedOperations.NormalVector * Constants.EPSILON;

            if (intersections == null) return preparedOperations;


            //determine n1 and n2 refractive indexes for given intersection

            //records objects encountered but not exited
            var containers = new List<Shape>();
            foreach (var intersection in intersections)
            {
                if (intersection == this)
                {
                    preparedOperations.N1 = !containers.Any() ? 1.0 : containers.Last().Material.RefractiveIndex;
                }

                if (containers.Contains(intersection.Object))
                {
                    containers.Remove(intersection.Object);
                }
                else
                {
                    containers.Add(intersection.Object);
                }

                if (intersection == this)
                {
                    preparedOperations.N2 = !containers.Any() ? 1.0 : containers.Last().Material.RefractiveIndex;
                }
            }

            return preparedOperations;
        }
    }
}