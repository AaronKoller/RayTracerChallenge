using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RayTracer.Shapes
{
    public class Group : Shape, IEnumerable<Shape>
    {
        private Shape[] _shapes = { };

        public int Count => _shapes.Length;


        public Shape this[int i]
        {
            get => _shapes[i];
            set
            {
                Array.Resize(ref _shapes, _shapes.Length + 1);
                _shapes[i] = value;
                _shapes[i].Parent = this;
            }
        }

        public void AddChild(Shape shape)
        {
            this[Count] = shape;
            //this.Parent = this;
            shape.Parent = this;

        }

        public override Tuple LocalNormalAt(Tuple point)
        {
            throw new Exception("LocalNormalAt Should not be called on groups as groups are abstract and don't have normal vectors.");
        }

        public override Intersections LocalIntersect(Ray ray)
        {

            var intersections = new Intersections();
            if(this.Count == 0)
                return null;

            foreach (var shape in this)
            {
                var localIntersection = shape.Intersect(ray);

                foreach (var intersection in localIntersection)
                {
                    intersections.Add(intersection);
                }
            }

            var sortedIntersections = new Intersections(intersections.OrderBy(x => x.T).ToArray());
            return sortedIntersections;
        }

        public override BoundingBox BoundsOf()
        {
            var boundingBox = new BoundingBox();
            foreach (var shape in _shapes)
            {
                boundingBox = boundingBox + shape.ParentSpaceBoundsOf();
            }

            return boundingBox;
        }

        public IEnumerator<Shape> GetEnumerator()
        {
            return ((IEnumerable<Shape>)_shapes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _shapes.GetEnumerator();
        }
    }
}