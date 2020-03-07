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
        
        public void renderScene()
        {
            unsafe
            {
                int pBackBuffer = 0;
                Application.Current.Dispatcher.Invoke(
                    () => { pBackBuffer = (int)bitmap.BackBuffer; }
                );

                for (int i = 0; i < YRes; i++)
                {
                    for (int j = 0; j < XRes; j++)
                    {
                        Color color = camera.renderPixel(j, i);
                        int color_data = colorToInt(color);
                            
                        Application.Current.Dispatcher.Invoke( () => 
                            {
                                bitmap.Lock();
                                *(int*)pBackBuffer = color_data;
                                bitmap.AddDirtyRect(new Int32Rect(j, i, 1, 1));
                                bitmap.Unlock();
                            }
                        );

                        pBackBuffer += 4;
                    }
                }
            }
        }
    }
}
