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

        public const int resX = 640;
        public const int resY = 480;

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            scene = TestScenes.Materials();
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
