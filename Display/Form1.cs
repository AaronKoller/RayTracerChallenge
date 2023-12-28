using System;
using RayTracer;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Renderer;
using Color = RayTracer.Color;

namespace Display
{
    public partial class Form1 : Form
    {

        private readonly int RGBValue = 255;
        public Form1()
        {
            var multiple = 2;

            InitializeComponent();
            //CreateBitmapAtRuntime(Render.clock, 100, 100, 5);
            //CreateBitmapAtRuntime(Render.ballistics, 900, 550, 1);
            //CreateBitmapAtRuntime(Render.renderedSphere, 200, 200, 1);
            //CreateBitmapAtRuntime(Render.firstWorld, 100 * multiple, 50 * multiple, 1);
            CreateBitmapAtRuntime(Render.Hexagon, 100 * multiple, 50 * multiple, 1);
        }

        private readonly PictureBox pictureBox1 = new PictureBox();

        public void CreateBitmapAtRuntime(Render render, int width, int height, int scale)
        {
            int widthResized = width * scale;
            int heightResized = height * scale;
            int padWidth = 16;
            int padHeight = 39;

            Canvas canvas = new Canvas(width, height, new Color(0, 0, 0));
            canvas.Create();

            switch (render)
            {
                case Render.ballistics:
                    Ballistics ballistics = new Ballistics(canvas);
                    ballistics.Fire();
                    break;
                case Render.clock:
                    ClockFace clock = new ClockFace(canvas, 12);
                    clock.Create();
                    break;

                case Render.renderedSphere:
                    RenderedSphere renderedSphere = new RenderedSphere(canvas);
                    renderedSphere.Do();
                    break;
                case Render.firstWorld:
                    FirstWorld firstWorld = new FirstWorld(canvas);
                    canvas = firstWorld.Create();
                    break;
                case Render.Hexagon:
                    Hexagon hexagon = new Hexagon(canvas);
                    canvas = hexagon.Create();
                    break;


            }

            Bitmap originalBitmap = GetDataPicture(width, height, canvas.Data);
            originalBitmap.Save("D:\\Src\\New folder\\20191129RayTracerChallenge\\src\\Images" + DateTime.Now.ToString("yyyyMMdd HHmmss") +".bmp");
            Bitmap resizedBitmap = ResizeBitmap(originalBitmap, widthResized, heightResized, InterpolationMode.NearestNeighbor);

            pictureBox1.Size = new Size(widthResized, heightResized);
            Controls.Add(pictureBox1);
            pictureBox1.Image = resizedBitmap;
            Width = widthResized + padWidth;
            Height = heightResized + padHeight;
        }

        public Bitmap GetDataPicture(int w, int h, Color[,] data)
        {
            Bitmap pic = new Bitmap(w, h, PixelFormat.Format32bppArgb);

            for (int x = 0; x < w; x++)
            {
                for (int y = h - 1; y > 0; y--)
                {
                    Color color = data[x, y];

                    var flippedY = h - y;
                    pic.SetPixel(x, flippedY, System.Drawing.Color.FromArgb(
                        (int)(color.RedChannel * RGBValue), 
                        (int)(color.GreenChannel * RGBValue), 
                        (int)(color.BlueChannel * RGBValue)));
                }
            }

            return pic;
        }

        private void DisplayExtremePixels(Canvas canvas)
        {

            int width = canvas.Data.GetLength(0);
            int height = canvas.Data.GetLength(1);

            canvas.WritePixel(0, 0, Color.Red);
            canvas.WritePixel(1, 1, Color.White);
            canvas.WritePixel(2, 2, Color.White);
            canvas.WritePixel(width - 1, 0, Color.Red);
            canvas.WritePixel(0, height - 1, Color.Red);
            canvas.WritePixel(width - 1, height - 1, Color.Red);
        }

        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height, InterpolationMode interpolationMode)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = interpolationMode;
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }
    }
}
