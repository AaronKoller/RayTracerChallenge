using System;
using System.Diagnostics.Tracing;
using RayTracer.Patterns;
using RayTracer.Shapes;

namespace RayTracer
{
    public class Material
    {
        public Color Color { get; set; }
        public double Ambient { get; set; }
        public double Diffuse { get; set; }
        public double Specular { get; set; }
        public double Shininess { get; set; }
        public double Reflective { get; set; }
        public Pattern Pattern { get; set; }
        public Pattern Pattern2 { get; set; }
        public double RefractiveIndex { get; set; }
        public double Transparency { get; set; }


        public Material()
        {
            Color = new Color(1, 1, 1);
            Ambient = .1;
            Diffuse = .9;
            Specular = .9;
            Shininess = 200.00;
            Reflective = 0;
            Transparency = 0;
            RefractiveIndex = 1.0;
        }

        public Color Lighting(Shape shape, Light light, Tuple point, Tuple eyeVector, Tuple normalVector, bool isShadow)
        {
            Color diffuse;
            Color specular;

            var color = this.Color;
            var color2 = Color.White;
            if (Pattern != null)
            {
                color = Pattern.PatternAtObject(shape, point);
                //color = Pattern.PatternAt(point);
            }

            if (Pattern2 != null)
            {
                color2 = Pattern2.PatternAtObject(shape, point);
                color = color + color2;
            }

            //Combine the source color with the light's color/intensity
            var effectiveColor = color * light.Intensity;

            //Find the direction to the light source
            var lightVector = (light.Position - point).Normalize();

            //compute the ambient contribution
            var ambient = effectiveColor * this.Ambient;

            if (isShadow) return ambient;

            //LightDotNormal represents the cosine of the angle between the light vector and the normal vector.
            // A negative number means the light is on the other side of the surface.
            var lightDotNormal = lightVector.Dot(normalVector);


            if (lightDotNormal < 0)
            {
                diffuse = Color.Black;
                specular = Color.Black;
            }
            else
            {
                //Computer the diffuse contribution
                diffuse = effectiveColor * this.Diffuse * lightDotNormal;

                //reflectDotEye represents the cosine of the angle between the reflection vector and the eye vector.
                // A negative number means the light reflects away from the eye.
                var reflectVector = -lightVector.Reflect(normalVector);
                var reflectDotEye = reflectVector.Dot(eyeVector);

                if (reflectDotEye <= 0)
                {
                    specular = Color.Black;
                }
                else
                {
                    //compute the specular contribution
                    var factor = Math.Pow(reflectDotEye, this.Shininess);
                    specular = light.Intensity * this.Specular * factor;
                }
            }

            return ambient + diffuse + specular;
        }
    }
}