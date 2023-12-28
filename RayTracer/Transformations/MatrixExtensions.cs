using System;

namespace RayTracer.Transformations
{
    public static class MatrixExtensions
    {
        public static Matrix Translation(this Matrix matrix, double x, double y, double z)
        {
            Matrix ident = matrix.Identity;
            ident.Data[0, 3] = x;
            ident.Data[1, 3] = y;
            ident.Data[2, 3] = z;

            return new Matrix(ident.Data);
        }
        public static Matrix Scale(this Matrix matrix, double scaleX, double scaleY, double scaleZ)
        {
            Matrix ident = matrix.Identity;
            ident.Data[0, 0] = scaleX;
            ident.Data[1, 1] = scaleY;
            ident.Data[2, 2] = scaleZ;

            return new Matrix(ident.Data);
        }

        public static Matrix Rotation_x(this Matrix matrix, double radians)
        {
            Matrix ident = matrix.Identity;
            ident.Data[1, 1] = Math.Cos(radians);
            ident.Data[2, 2] = Math.Cos(radians);
            ident.Data[1, 2] = -Math.Sin(radians);
            ident.Data[2, 1] = Math.Sin(radians);

            return new Matrix(ident.Data);
        }

        public static Matrix Rotation_y(this Matrix matrix, double radians)
        {
            Matrix ident = matrix.Identity;
            ident.Data[0, 0] = Math.Cos(radians);
            ident.Data[2, 2] = Math.Cos(radians);
            ident.Data[0, 2] = Math.Sin(radians);
            ident.Data[2, 0] = -Math.Sin(radians);

            return new Matrix(ident.Data);
        }

        public static Matrix Rotation_z(this Matrix matrix, double radians)
        {
            Matrix ident = matrix.Identity;
            ident.Data[0, 0] = Math.Cos(radians);
            ident.Data[1, 1] = Math.Cos(radians);
            ident.Data[0, 1] = -Math.Sin(radians);
            ident.Data[1, 0] = Math.Sin(radians);

            return new Matrix(ident.Data);
        }

        public static Matrix Shearing(this Matrix matrix,
            double shearXy, double shearXz,
            double shearYx, double shearYz,
            double shearZx, double shearZy)
        {
            Matrix ident = matrix.Identity;
            ident.Data[1, 0] = shearYx;
            ident.Data[2, 0] = shearZx;
            ident.Data[0, 1] = shearXy;
            ident.Data[2, 1] = shearZy;
            ident.Data[0, 2] = shearXz;
            ident.Data[1, 2] = shearYz;

            return new Matrix(ident.Data);
        }
    }
}
