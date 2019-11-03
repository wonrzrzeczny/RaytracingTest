using Raytracing.Algebra;
using Raytracing.Surfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Raytracing
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        Render render;
        Scene scene;
        Camera camera;
        Thread updateThread;

        public const int resX = 1280;
        public const int resY = 720;

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            Random random = new Random();
            scene = new Scene(new Color(255, 255, 255));

            SurfaceMaterial solid = new SurfaceAbsorptive(new Color(128, 128, 128));
            SurfaceMaterial solidRed = new SurfaceAbsorptive(new Color(255, 128, 128));
            SurfaceMaterial solidBlue = new SurfaceAbsorptive(new Color(128, 128, 255));
            SurfaceMaterial reflective = new SurfaceReflective(scene);
            SurfaceMaterial hybrid = new SurfaceMaterialProduct(solid, reflective, new Vector3(0.2, 0.2, 0.2));
            SurfaceMaterial hybridRed = new SurfaceMaterialProduct(solidRed, reflective, new Vector3(0.2, 0.2, 0.2));
            SurfaceMaterial hybridBlue = new SurfaceMaterialProduct(solidBlue, reflective, new Vector3(0.2, 0.2, 0.2));

            scene.addSurface(new Surface(new SurfacePlane(-Vector3.Up, Vector3.Forward, Vector3.Right), hybrid));
            //scene.addSurface(new Surface(new SurfaceSphere(new Vector3(-10, 10, 30), 4), hybridRed));
            //scene.addSurface(new Surface(new SurfaceSphere(new Vector3(10, 10, 30), 4), hybridBlue));
            for (int i = 0; i < 4; i++)
            {
                scene.addSurface(new Surface
                                    (new SurfaceSphere
                                        (new Vector3(random.Next(-50, 50), random.Next(5, 25), random.Next(60, 80)), random.Next(4, 14)),
                                     new SurfaceMaterialProduct
                                        (new SurfaceAbsorptive(new Color((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))), 
                                            reflective, 0.3 * Vector3.One)));
            }

            camera = new Camera(new Vector3(0, 0, 0), 80, resX, resY, scene);
            render = new Render(camera, resX, resY);

            MainWindow window = new MainWindow();
            window.Content = render.renderImage;
            window.Show();

            updateThread = new Thread(new ThreadStart(Update));
            updateThread.Start();
        }
        
        void Update()
        {
            Dispatcher.Invoke(render.renderScene);
            Thread.Sleep(10);
            while (true)
            {

            }
        }
    }
}
