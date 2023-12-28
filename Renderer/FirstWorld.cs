using System;
using System.Collections.Generic;
using System.Text;
using RayTracer;
using RayTracer.Patterns;
using Tuple = RayTracer.Tuple;
using RayTracer.Shapes;
using RayTracer.Transformations;

namespace Renderer
{
    public class FirstWorld
    {
        private Canvas _canvas;

        public FirstWorld(Canvas canvas)
        {
            _canvas = canvas;
        }
        public Canvas Create()
        {

            var floorPlane = new Plane();
            floorPlane.Material = new Material();
            floorPlane.Material.Color = new Color(1, .9, .9);
            floorPlane.Material.Specular = 0;
            //floorPlane.Material.Pattern = new RingPattern(new Color(.7,.7,.7), Color.White);
            floorPlane.Material.Reflective = .2;
            floorPlane.Material.Pattern2 = new CheckersPatterns(Color.White, new Color(.5,.5,.5));
            //floorPlane.Material.Pattern2.Transform = Matrix.Transform.Scale(.3, .3, .3) * Matrix.Transform.Rotation_z(Math.PI/4);

            var cone = new Cone
            {
                Maximum = 0,
                Minimum = -1
            };
            cone.Transform = Matrix.Transform.Translation(0, 1, 0) * Matrix.Transform.Rotation_z(Math.PI / 6) *
                             Matrix.Transform.Rotation_x(Math.PI / 6);
            cone.Material.Color = new Color(.9,.5,.1);
            cone.Material.Reflective = .4;

            var cube = new Cube();
            cube.Material.Pattern = new StripePattern(Color.Blue, Color.Black);
            cube.Material.Pattern.Transform =
                Matrix.Transform.Scale(.5, .5, .5) * Matrix.Transform.Rotation_x(Math.PI / 6) * Matrix.Transform.Rotation_y(Math.PI / 6);
            cube.Material.Reflective = .5;
            cube.Transform = Matrix.Transform.Rotation_x(Math.PI/12) * Matrix.Transform.Rotation_y(Math.PI / 12);

            var cylinder = new Cylinder
            {
                Minimum = 0,
                Maximum = 2,
                Closed = true
            };
            cylinder.Material.Reflective = .2;
            cylinder.Material.Color = new Color(.2,.6,.9);
            cylinder.Transform = Matrix.Transform.Scale(.5, .5, .5) * Matrix.Transform.Translation(-6, .5, 0);


            var glassSphere = new GlassSphere();
            glassSphere.Transform = Matrix.Transform.Scale(4, 4, 4) * Matrix.Transform.Translation(0,0,2.5);
            glassSphere.Material.Transparency = .9;
            glassSphere.Material.Reflective = .9;
            glassSphere.Material.Diffuse = .1;
            glassSphere.Material.Specular = 1;
            glassSphere.Material.Shininess = 300;
            glassSphere.Material.Color = new Color(0,0,.5);

            var emptySphere = new Sphere();
            emptySphere.Transform = Matrix.Transform.Scale(2, 2, 2) * Matrix.Transform.Translation(-3, 0, 4);
            emptySphere.Material.Color = new Color(0, 1, 0);
            emptySphere.Material.Reflective = .9;


            //good
            var floor = new Sphere();
            floor.Transform = Matrix.Transform.Scale(10, .01, 10);
            floor.Material = new Material();
            floor.Material.Color = new Color(1,.9,.9);
            floor.Material.Specular = 0;

            //good
            var leftWall = new Sphere();
            leftWall.Transform = Matrix.Transform.Translation(0, 0, 5) * 
                                 Matrix.Transform.Rotation_y(-Math.PI/4) * 
                                 Matrix.Transform.Rotation_x(Math.PI/2) * 
                                 Matrix.Transform.Scale(10,.01,10);
            leftWall.Material = floor.Material;


            //good
            var rightWall = new Sphere();
            rightWall.Transform = Matrix.Transform.Translation(0,0,5) * 
                                  Matrix.Transform.Rotation_y(Math.PI/4) * 
                                  Matrix.Transform.Rotation_x(Math.PI/2) *
                                  Matrix.Transform.Scale(10,.01,10);
            leftWall.Material = floor.Material;
            
            //good
            var middleSphere = new Sphere();
            middleSphere.Transform = Matrix.Transform.Translation(-.5,1,.5);
            middleSphere.Material = new Material();
            middleSphere.Material.Color = new Color(.1,1,.5);
            middleSphere.Material.Diffuse = .7;
            middleSphere.Material.Specular = .3;
            middleSphere.Material.Pattern = new CheckersPatterns(Color.Black, Color.Red);
            middleSphere.Material.Reflective = .6;
            //middleSphere.Material.Pattern2 = new GradientPattern(Color.Black, new Color(.5,.5,.5));
            middleSphere.Material.Pattern.Transform = Matrix.Transform.Scale(.3, .3, .3) * Matrix.Transform.Rotation_y(Math.PI/6) * Matrix.Transform.Rotation_z(Math.PI / 2) * Matrix.Transform.Rotation_x(Math.PI / 6);
            
            //middleSphere.Material.Pattern2.Transform = Matrix.Transform.Scale(.3, .3, .3) * Matrix.Transform.Rotation_y(Math.PI/6) * Matrix.Transform.Rotation_z(Math.PI / 2) * Matrix.Transform.Rotation_x(Math.PI / 6);

            //good
            var rightSphere = new Sphere();
            rightSphere.Transform = Matrix.Transform.Translation(1.5,.5,-.5) * 
                Matrix.Transform.Scale(.5,.5,.5);
            rightSphere.Material = new Material();
            rightSphere.Material.Color = new Color(.5,1,.1);
            rightSphere.Material.Diffuse = .7;
            rightSphere.Material.Specular = .3;

            var leftSphere = new Sphere();
            leftSphere.Transform = Matrix.Transform.Translation(-1.5,.33,1.5) * 
                Matrix.Transform.Scale(1,.5,.33);
            leftSphere.Material = new Material();
            leftSphere.Material.Color = new Color(1,0,0);
            leftSphere.Material.Diffuse = .7;
            leftSphere.Material.Specular = .8;
            leftSphere.Material.Reflective = .1;


            var spheres = new List<Shape>
            {
                //floor,
                //leftWall,
                //rightWall,
                floorPlane,
                //middleSphere,
                //rightSphere,
                //leftSphere,
                cone,

                //cylinder,
                //glassSphere,
                //emptySphere,
                //cube
            };

            var world = new World();
            world.Light = new Light(RayTracer.Tuple.Point(-10,10,-10), Color.White );
            world.Shapes = spheres;

            var camera = new Camera(_canvas.Width,_canvas.Height, Math.PI/ 3);
            camera.Transform = new Matrix().ViewTransform(
                Tuple.Point(2,1.8,-5),
                Tuple.Point(0,1,0),
                Tuple.Vector(0,1,0));

            var image = camera.Render(world);

            return image;
        }
    }
}
