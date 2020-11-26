using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Raytracing
{
    //Fast pixel by pixel render
    public class Render
    {
        public Image RenderImage { get; private set; }
        private readonly Color[,] colorArray;

        private WriteableBitmap bitmap;
        private readonly Camera camera;

        public int XRes { get; }
        public int YRes { get; }

        public Render(Camera camera, int xRes, int yRes)
        {
            RenderImage = new Image();
            bitmap = new WriteableBitmap(xRes, yRes, 96.0, 96.0, PixelFormats.Bgr32, null);
            RenderImage.Source = bitmap;
            colorArray = new Color[xRes, yRes];
            this.camera = camera;
            XRes = xRes;
            YRes = yRes;
        }
        
        private int colorToInt(Color color)
        {
            return (color.R << 16) + (color.G << 8) + color.B; 
        }
        
        public void renderScene(int threadCount)
        {
            int[,] colorData = new int[XRes, YRes];

            Thread[] threads = new Thread[threadCount];
            int batchSize = XRes / threadCount;
            for (int i = 0; i < threadCount; i++)
            {
                int _i = i; // Capture into the lambda by copy
                threads[i] = new Thread(new ThreadStart(() =>
                {
                    int xMin = batchSize * _i;
                    int xMax = Math.Min(batchSize * (_i + 1), XRes);
                    for (int x = xMin; x < xMax; x++)
                        for (int y = 0; y < YRes; y++)
                            colorData[x, y] = colorToInt(camera.renderPixel(x, y));
                }));
                threads[i].Start();
            }

            for (int i = 0; i < threadCount; i++)
                threads[i].Join();

            unsafe
            {
                int pBackBuffer = (int)bitmap.BackBuffer;
                bitmap.Lock();

                for (int i = 0; i < YRes; i++)
                {
                    for (int j = 0; j < XRes; j++)
                    {
                        *(int*)pBackBuffer = colorData[j, i];
                        pBackBuffer += 4;
                    }
                }

                bitmap.AddDirtyRect(new Int32Rect(0, 0, XRes, YRes));
                bitmap.Unlock();
            }
        }
    }
}
