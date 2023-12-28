using System;

namespace RayTracer
{
    public class Camera
    {
        private double _halfWidth;
        private double _halfHeight;

        public Camera(double hSize, double vSize, double fieldOfView)
        {
            HSize = hSize;
            VSize = vSize;
            FieldOfView = fieldOfView;
        }

        public Matrix Transform { get; set; } = new Matrix().Identity;
        public double FieldOfView { get; set; }
        public double VSize { get; set; }
        public double HSize { get; set; }
        public double HalfWidth => _halfWidth;
        public double HalfHeight => _halfHeight;

        public double PixelSize => CalculatePixelSize();

        private double CalculatePixelSize()
        {
            var halfView = Math.Tan(FieldOfView / 2);
            var aspect = HSize / VSize;

            if (aspect > 1)
            {
                _halfWidth = halfView;
                _halfHeight = halfView / aspect;
            }
            else
            {
                _halfWidth = halfView * aspect;
                _halfHeight = halfView;
            }

            return (_halfWidth * 2) / HSize;
        }

        public Ray RayForPixel(double pixelX, double pixelY)
        {
            var pixelSize = PixelSize;

            //The offset from the edge of the canvas to the pixel's center
            var xOffset = (pixelX + .5) * pixelSize;
            var yOffset = (pixelY + .5) * pixelSize;

            //The untransformed coordinates of the pixel in the world space.
            //(remember that the camera looks toward -z, so +x is to the *left*)
            var worldX = _halfWidth - xOffset;
            var worldY = _halfHeight - yOffset;

            //using the camera matrix, transform the canvas point and the origin, 
            //and then compute the ray's direction vector.
            //(remember that the canvas is at z=-1)
            var pixel = Transform.Inverse() * Tuple.Point(worldX, worldY, -1);
            var origin = Transform.Inverse() * Tuple.OriginPoint;
            var direction = (pixel - origin).Normalize();

            var ray = new Ray(origin,direction);

            return ray;
        }

        public Canvas Render(World world)
        {
            var image = new Canvas(HSize, VSize).Create();

            for (int y = 0; y < VSize; y++)
            {
                for (int x = 0; x < HSize; x++)
                {
                    var ray = RayForPixel(x, y);
                    var color = world.ColorAt(ray);  //Todo: This should be something higher like 5.  (prevents infinite recursion)
                    image.WritePixel(x,y,color);
                }   
            }
             return image;
        }
    }
}