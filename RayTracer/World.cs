using RayTracer.Shapes;
using RayTracer.Transformations;
using System;
using System.Collections.Generic;

namespace RayTracer
{
    public class World
    {

        //we can support a list of lights and have shadeHit() iterate over each of the lights - location 3138
        public Light Light { get; set; } = new Light(Tuple.Point(-10, 10, -10), Color.White);
        public List<Shape> Shapes { get; set; } = new List<Shape>
        {
            new Sphere
            {
                Material = new Material {Color = new Color(.8, 1.0, 0.6), Diffuse = .7, Specular = 0.2}
            },
            new Sphere
            {
                Transform = Matrix.Transform.Scale(.5,.5,.5)
            }
        };

        public Intersections IntersectWorld(Ray ray)
        {
            List<Intersection> intersections = new List<Intersection>();

            foreach (Shape sphere in Shapes)
            {
                var intersection = sphere.Intersect(ray); //added due to null exceptions
                if (intersection == null) continue;

                intersections.AddRange(intersection);
            }

            return new Intersections(intersections.ToArray());
        }

        public Color ShadeHit(Shape shape, Intersection.PreComputation comps, int remaining = Constants.RECURSIONDEPTH)
        {

            bool shadowed = IsShadowed(comps.OverPoint);

            //var shadeColor = preparedComputations.Shape.Material.Lighting( 
            //    shape, //not sure if this is correct at location 4315 step 3
            //    Light,
            //    preparedComputations.OverPoint,
            //    preparedComputations.EyeVector,
            //    preparedComputations.NormalVector, 
            //    shadowed);

            Color surface = comps.Shape.Material.Lighting(
                comps.Shape, //not sure if this is correct at location 4315 step 3
                Light,
                comps.OverPoint,
                comps.EyeVector,
                comps.NormalVector,
                shadowed);

            Color reflected = ReflectedColor(comps, remaining);
            Color refracted = RefractedColor(comps, remaining);

            var material = comps.Shape.Material;

            if (material.Reflective > 0 && material.Transparency > 0)
            {
                var reflectance = Intersections.Schlick(comps);
                return surface + reflected * reflectance + refracted * (1 - reflectance);
            }

            return surface + reflected + refracted;
        }

        public Color ColorAt(Ray ray, int remaining = Constants.RECURSIONDEPTH)
        {
            Intersections intersections = IntersectWorld(ray);
            if (intersections.Count == 0)
            {
                return Color.Black;
            }

            Intersection hit = intersections.Hit();
            if (hit == null)
            {
                return Color.Black;
            }

            Intersection.PreComputation preComputations = hit.PrepareComputations(ray, intersections);
            //var color = ShadeHit(hit.Object, preComputations);
            return ShadeHit(hit.Object, preComputations, remaining);
            //return color;
        }

        public bool IsShadowed(Tuple point)
        {
            Tuple vector = Light.Position - point;
            double distance = vector.Magnitude();
            Tuple direction = vector.Normalize();

            Ray ray = new Ray(point, direction);
            Intersections intersections = IntersectWorld(ray);
            Intersection hit = intersections.Hit();

            bool result = hit != null && hit.T < distance;
            return result;
        }

        public Color ReflectedColor(Intersection.PreComputation precomputation, int remaining = Constants.RECURSIONDEPTH)
        {
            if (remaining <= 0)
            {
                return Color.Black;
            }

            if (Math.Abs(precomputation.Shape.Material.Reflective) < Constants.EPSILON)
            {
                return Color.Black;
            }

            Ray reflectRay = new Ray(precomputation.OverPoint, precomputation.ReflectVector);
            Color colorAt = ColorAt(reflectRay, --remaining); //4814 says remaining - 1
            Color reflectColor = colorAt * precomputation.Shape.Material.Reflective;
            return reflectColor;
        }

        public Color RefractedColor(Intersection.PreComputation comps, int remaining = Constants.RECURSIONDEPTH)
        {
            if (remaining == 0)
            {
                return Color.Black;
            }

            //Internal refraction
            //Find the ration of the first index of the refraction to the second.
            //(Yup, this is inverted from the definition of Snell's law)
            double nRatio = comps.N1 / comps.N2;
            //cost(thetaI) is the same as the dot product of the two vectors
            double cosI = comps.EyeVector.Dot(comps.NormalVector);
            //Find the sin(thetaI)^2 via the trigonometric identity
            double sin2T = Math.Pow(nRatio, 2) * (1 - Math.Pow(cosI, 2));
            if (sin2T > 1)
            {
                return Color.Black;
            }

            //Refracted color
            //Find the cost(thetaI) via the trigonometric identity
            var cosT = Math.Sqrt(1.0 - sin2T);
            //compute the direction of the refracted ray
            var direction = comps.NormalVector * (nRatio * cosI - cosT) - comps.EyeVector * nRatio;
            //Create the refracted array
            var refractRay = new Ray(comps.UnderPoint, direction);
            //Find the color of the ray, making sure to multiply
            //by the transparency value to account for any opacity
            var resultColor = ColorAt(refractRay, remaining - 1) * comps.Shape.Material.Transparency;

            return resultColor;
        }
    }
}