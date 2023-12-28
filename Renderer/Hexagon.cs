using System;
using System.Collections.Generic;
using RayTracer;
using RayTracer.Shapes;
using RayTracer.Transformations;
using Tuple = RayTracer.Tuple;

namespace Renderer
{
    public class Hexagon
    {
        private Canvas _canvas;

        public Hexagon(Canvas canvas)
        {
            _canvas = canvas;
        }

        public Canvas Create()
        {

            //dunno why the cylinders are transparent when in my firstWorld() class they are not transparent
            var world = new World();
            world.Light = new Light(RayTracer.Tuple.Point(-10, 10, -10), Color.White);
            world.Shapes = new List<Shape> {HexagonShape()};

            var camera = new Camera(_canvas.Width, _canvas.Height, Math.PI / 3);
            camera.Transform = new Matrix().ViewTransform(
                //Tuple.Point(0, 1.8, -5),
                Tuple.Point(2, 4, -5),
                Tuple.Point(0, 1, 0),
                Tuple.Vector(0, 1, 0));

            var image = camera.Render(world);


            return image;
        }


        private Shape HexagonSide()
        {
            var side = new Group();

            side.AddChild(HexagonCorner());
            side.AddChild(HexagonEdge());

            return side;
        }

        private Shape HexagonShape()
        {
            var hex = new Group();

            for (int i = 0; i < 6; i++)
            {
                var side = HexagonSide();
                side.Transform = Matrix.Transform.Rotation_y(i * Math.PI / 3);
                hex.AddChild(side);
            }

            return hex;
        }

        private Shape HexagonEdge()
        {
            var edge = new Cylinder
            {
                Closed = true,
                Minimum = 0,
                Maximum = 1,
                Transform = Matrix.Transform.Translation(0,0,-1) *
                            Matrix.Transform.Rotation_y(-Math.PI/6) * 
                            Matrix.Transform.Rotation_z(-Math.PI/2) * 
                            Matrix.Transform.Scale(.25,1,.25)
            };
            return edge;
        }

        private Shape HexagonCorner()
        {
            var corner = new Sphere
            {
                Transform = Matrix.Transform.Translation(0,0,-1) * Matrix.Transform.Scale(.25, .25, .25)
            };

            return corner;
        }

    }
}