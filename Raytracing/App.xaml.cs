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

        public const int resX = 320;
        public const int resY = 160;

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            Console.WriteLine(Vector3.Forward.reflect(new Vector3(1, 1, 1)));

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
            scene.addSurface(new Surface(new SurfaceSphere(new Vector3(-20, 10, 50), 4), hybridRed));
            scene.addSurface(new Surface(new SurfaceSphere(new Vector3(20, 10, 50), 4), hybridBlue));
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
