namespace RayTracer
{
    public class Color
    {
        public static Color Black => new Color(0,0,0);
        public static Color White => new Color(1,1,1);
        public static Color Red => new Color(1,0,0);
        public static Color Green => new Color(0,1,0);
        public static Color Blue => new Color(0,0,1);

        public double RedChannel { get; set; }
        public double BlueChannel { get; set; }
        public double GreenChannel { get; set; }

        public Color()
        {
            
        }
        public Color(double redChannel, double greenChannel, double blueChannel)
        {
            RedChannel = redChannel;
            GreenChannel = greenChannel;
            BlueChannel = blueChannel;
        }

        public static Color operator +(Color color1, Color color2)
        {
            return new Color
            {
                RedChannel = color1.RedChannel + color2.RedChannel,
                GreenChannel = color1.GreenChannel + color2.GreenChannel,
                BlueChannel = color1.BlueChannel + color2.BlueChannel,
            };
        }

        public static Color operator -(Color color1, Color color2)
        {
            return new Color
            {
                RedChannel = color1.RedChannel - color2.RedChannel,
                GreenChannel = color1.GreenChannel - color2.GreenChannel,
                BlueChannel = color1.BlueChannel - color2.BlueChannel,
            };
        }

        public static Color operator *(Color color1, Color color2)
        {
            return new Color
            {
                RedChannel = color1.RedChannel * color2.RedChannel,
                GreenChannel = color1.GreenChannel * color2.GreenChannel,
                BlueChannel = color1.BlueChannel * color2.BlueChannel,
            };
        }

        public static Color operator *(Color color1, double multiple)
        {
            return new Color
            {
                RedChannel = color1.RedChannel * multiple,
                GreenChannel = color1.GreenChannel * multiple,
                BlueChannel = color1.BlueChannel * multiple,
            };
        }
    }
}