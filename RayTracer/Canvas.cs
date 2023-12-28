namespace RayTracer
{
    public class Canvas
    {
        public Color[,] Data => _canvasData;
        private readonly int _width;
        private readonly int _height;
        private readonly Color _color;
        private Color[,] _canvasData;
        private readonly int _originOffset;

        public int Width => _width;
        public int Height => _height;


        public Canvas(double width, double height) : this((int)width, (int)height, Color.Black) { }

        public Canvas(int width, int height, Color color)
        {
            _width = width;
            _height = height;
            _color = color;
            _originOffset = height - 1;
        }

        public Canvas Create()
        {
            _canvasData = new Color[_width, _height];
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _canvasData[x, y] = _color;
                }
            }

            return this;
        }

        public void WritePixel(double x, double y, Color color)
        {
            WritePixel((int)x, (int)y, color);
        }

        public void WritePixel(int x, int y, Color color)
        {
            if (x < 0 || x > _width - 1 || y < 0 || y > _height - 1) return;
            //x must be from 1 to width - 1 as 


            var clampedColor = clampColor(color);

            _canvasData[x, _originOffset - y] = clampedColor;
        }

        private Color clampColor(Color color)
        {
            var newColor = Color.White;
            newColor.RedChannel = clamp(color.RedChannel, 0, 1);
            newColor.GreenChannel = clamp(color.GreenChannel, 0, 1);
            newColor.BlueChannel = clamp(color.BlueChannel, 0, 1);
            return newColor;
        }

        private double clamp(double input, double low, double high)
        {
            double clamped = input;
            if (input > high)
            {
                clamped = high;
            }

            ;
            if (input < low)
            {
                clamped = low;
            }
            return clamped;
        }

    }
}