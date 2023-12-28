using System;
using System.Collections.Generic;
using System.Text;
using RayTracer;
using RayTracer.Shapes;
using RayTracer.Transformations;
using Tuple = RayTracer.Tuple;

namespace Renderer
{
    public class RenderedSphere
    {
        private readonly Canvas _canvas;

        public RenderedSphere(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Do()
        {
            var rayOrigin = Tuple.Point(0, 0, -5);
            var wallZ = 10.0;
            var wallSize = 7.0;

            var pixelSize = wallSize / _canvas.Height;
            var half = wallSize / 2;


            for (int y = 0; y < _canvas.Height; y++)
            {
                var worldY = half - pixelSize * y;

                for (int x = 0; x < _canvas.Width; x++)
                {
                    var worldX = -half + pixelSize * x;
                    var position = Tuple.Point(worldX, worldY, wallZ);
                    var ray = new Ray(rayOrigin, (position - rayOrigin).Normalize());

                    var sphere = new Sphere();
                    //sphere.Material.Color = new Color(1, .2,1); //purple
                    sphere.Material.Color = new Color(.5, .5,.5); //grey

                    var lightPosition = Tuple.Point(-10, 10, -10);
                    var lightColor = new Color(1,1,1);
                    var light = new Light(lightPosition, lightColor);


                    //sphere.Transform = Matrix.Transform.Scale(1,0.5,1); //squasked Y
                    //sphere.Transform = Matrix.Transform.Scale(.5,1,1); //squased X
                    sphere.Transform = Matrix.Transform.Rotation_z(Math.PI / 4) * Matrix.Transform.Scale(.5, 1, 1); //diagonal bottom left to top right
                    //sphere.Transform = Matrix.Transform.Shearing(1, 0, 0, 0, 0, 0) * Matrix.Transform.Scale(.5, 1, 1);  //diagonal top left to bottom right
                    //sphere.Transform = Matrix.Transform.Translation(0, 0, 10);

                    var intersect = sphere.LocalIntersect(ray);



                    if (intersect.Count > 0)
                    {
                        var hit = intersect.Hit();
                        var point = ray.Position(hit.T);
                        var normal = hit.Object.NormalAt(point);
                        var eye = -ray.Direction;

                        var newColor = hit.Object.Material.Lighting(hit.Object, light, point, eye, normal, false);


                        _canvas.WritePixel(x, y, newColor);

                    }
                }
            }
        }
    }
}
