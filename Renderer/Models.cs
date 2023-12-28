using Tuple = RayTracer.Tuple;

namespace Renderer
{
    public class Environment
    {
        public Environment(Tuple gravity, Tuple wind)
        {
            Gravity = gravity;
            Wind = wind;
        }

        public Tuple Gravity { get; set; }
        public Tuple Wind { get; set; }
    }

    public class Projectile
    {
        public Projectile(Tuple position, Tuple velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public Tuple Position { get; set; }
        public Tuple Velocity { get; set; }

        public override string ToString()
        {
            return $"{Position} (v={Velocity})";
        }
    }
}
