using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RayTracer
{
    public class Intersections :IEnumerable<Intersection>
    {
        private Intersection[] _intersections = { };

        public Intersection this[int i]
        {
            get => _intersections[i];
            set
            {
                Array.Resize(ref _intersections, _intersections.Length + 1);
                _intersections[i] = value;
            }
        }

        public int Count => _intersections.Length;

        public Ray SavedRay { get; set; }

        public Intersections() { }

        public Intersections(Intersection[] toArray)
        {
            _intersections = toArray;
        }

        public Intersection Hit()
        {
            return _intersections.Where(i => i.T > 0).OrderBy(j => j.T).FirstOrDefault();
        }

        public IEnumerator<Intersection> GetEnumerator()
        {
           return ((IEnumerable<Intersection>)_intersections).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _intersections.GetEnumerator();
        }

        public void Add(Intersection intersection)
        {
            this[Count] = intersection;
        }

        public static double Schlick(Intersection.PreComputation comps)
        {
            //find the cosine of the angle between the eye and the normal vectors
            var cos = comps.EyeVector.Dot(comps.NormalVector);

            //TotalInternal reflection can only occur if n1 > n2
            if (comps.N1 > comps.N2)
            {
                var n = comps.N1 / comps.N2;
                var sin2T = Math.Pow(n, 2) * (1.0 - Math.Pow(cos, 2));
                if (sin2T > 1.0) return 1;

                //compute the cosine of thetaT using the trig identity
                var cosT = Math.Sqrt(1.0 - sin2T);

                //when n1 > n2, use cost(ThetaT) instead
                cos = cosT;
            }

            var ro = Math.Pow((comps.N1 - comps.N2) / (comps.N1 + comps.N2), 2);
            var number = ro + (1 - ro) * Math.Pow(1 - cos, 5);
            return number;
        }
    }
}