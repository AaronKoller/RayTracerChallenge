using System;
using RayTracer;
using Tuple = RayTracer.Tuple;

namespace Renderer
{
    public class Ballistics
    {
        private readonly Canvas _canvas;

        public Ballistics(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Fire()
        {
            var projectile = new Projectile(RayTracer.Tuple.Point(0, 1, 0), RayTracer.Tuple.Vector(1, 1.8, 0).Normalize() * 11.25);
            var environment = new Environment(RayTracer.Tuple.Vector(0, -0.1, 0), Tuple.Vector(-0.00, 0.00, 0));
            int i = 0;
            while (projectile.Position.Y >= 0)
            {
                i++;
                var xPosition = (int) Math.Floor(projectile.Position.X);
                var yPosition = (int) Math.Floor(projectile.Position.Y);
                _canvas.WritePixel(xPosition, yPosition, Color.Red);
                //Console.WriteLine($"{i} - {projectile}");
                projectile = Update(projectile, environment);
            }
        }


        private static Projectile Update(Projectile projectile, Environment environment)
        {
            return new Projectile(
                projectile.Position + projectile.Velocity,
                projectile.Velocity + environment.Gravity + environment.Wind
            );
        }
    }
}
