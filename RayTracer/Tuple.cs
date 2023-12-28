using System;

namespace RayTracer
{
    public class Tuple
    {
        public static Tuple Point(double x, double y, double z)
        {
            return new Tuple(x, y, z, 1);
        }

        public static Tuple Vector(double x, double y, double z)
        {
            return new Tuple(x, y, z, 0);
        }

        public Tuple(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public bool IsPoint => Math.Abs(W - 1) < Constants.EPSILON;
        public bool IsVector => Math.Abs(W) < Constants.EPSILON;
        public static Tuple Zero => new Tuple(0,0,0,0);

        public static readonly Tuple OriginPoint = Point(0, 0, 0);

        public Matrix IdentityMatrix => new Matrix().GenerateIdentityMatrix(4); 
        public static Tuple operator *(Matrix matrix, Tuple tuple)
        {

            var tupleMatrix = new Matrix(tuple);
            var resultMatrix = matrix * tupleMatrix;

            return new Tuple(
                resultMatrix.Data[0,0],
                resultMatrix.Data[1,0],
                resultMatrix.Data[2,0],
                resultMatrix.Data[3,0]);
        }

        public static Tuple operator +(Tuple tuple1, Tuple tuple2)
        {
            if ((tuple1.W - 1).EqualsD(0) && (tuple2.W - 1).EqualsD(0)) throw new InvalidOperationException("Two points cannot be added.");

            return new Tuple(
                tuple1.X + tuple2.X,
                tuple1.Y + tuple2.Y,
                tuple1.Z + tuple2.Z,
                tuple1.W + tuple2.W
            );
        }

        public static Tuple operator -(Tuple tuple1, Tuple tuple2)
        {
            if ((tuple1.W).EqualsD(0) && (tuple2.W - 1).EqualsD(0))
            {
                throw new InvalidOperationException("A vector cannot be subtracted from a point.");
            }

            return new Tuple(
                tuple1.X - tuple2.X,
                tuple1.Y - tuple2.Y,
                tuple1.Z - tuple2.Z,
                tuple1.W - tuple2.W
            );
        }

        public static Tuple operator -(Tuple tuple1)
        {
            return new Tuple(
                -tuple1.X,
                -tuple1.Y,
                -tuple1.Z,
                -tuple1.W);
        }

        public static Tuple operator *(Tuple tuple1, double multiplier)
        {
            return new Tuple(
                tuple1.X * multiplier,
                tuple1.Y * multiplier,
                tuple1.Z * multiplier,
                tuple1.W * multiplier);
        }

        public static Tuple operator /(Tuple tuple1, double divisor)
        {
            return new Tuple(
                tuple1.X / divisor,
                tuple1.Y / divisor,
                tuple1.Z / divisor,
                tuple1.W / divisor);
        }


        public double Magnitude()
        {
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2) + Math.Pow(W, 2));
        }

        public Tuple Normalize()
        {
            var m = Magnitude();
            return this / m;
        }

        public double Dot(Tuple other)
        {
            return
                this.X * other.X +
                this.Y * other.Y +
                this.Z * other.Z +
                this.W * other.W;
        }

        public Tuple Cross(Tuple v)
        {
            return new Tuple(
                this.Y * v.Z - this.Z * v.Y,
                this.Z * v.X - this.X * v.Z,
                this.X * v.Y - this.Y * v.X,
                0
            );
        }

        public override string ToString()
        {
            return $"[{X}, {Y}, {Z}],({W})";
        }


        public override bool Equals(object obj)
        {
            Tuple t = obj as Tuple;
            Tuple self = this as Tuple;
            if (t == null || self == null)
            {
                return false;
            }
            else
            {
                return (t.X.EqualsD(self.X)
                    && t.Y.EqualsD(self.Y)
                    && t.Z.EqualsD(self.Z)
                    && t.W.EqualsD(self.W));
            }
        }

        public Tuple Reflect(Tuple normal)
        {
            return this - normal * 2 * Dot(normal);
        }
    }
}
