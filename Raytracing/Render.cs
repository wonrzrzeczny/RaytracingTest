using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        Random random = new Random();
        public void renderScene()
        {
            try
            {
                bitmap.Lock();

                unsafe
                {
                    // Get a pointer to the back buffer.
                    int pBackBuffer = (int)bitmap.BackBuffer;

                    for (int i = 0; i < YRes; i++)
                    {
                        for (int j = 0; j < XRes; j++)
                        {
                            // Compute the pixel's color.
                            Color color = camera.renderPixel(j, i);
                            int color_data = (color.R << 16) + (color.G << 8) + color.B;// (colorArray[i, j].R << 16) + (colorArray[i, j].G << 8) + colorArray[i, j].B;

                            // Assign the color data to the pixel.
                            *(int*)pBackBuffer = color_data;

                            pBackBuffer += 4;
                        }
                    }
                }

                // Specify the area of the bitmap that changed.
                bitmap.AddDirtyRect(new Int32Rect(0, 0, XRes, YRes));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                bitmap.Unlock();
            }
        }
    }
}
