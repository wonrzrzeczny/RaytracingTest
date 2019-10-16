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

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            scene = new Scene(new Color(255, 255, 255));
            camera = new Camera(new Algebra.Vector3(0, 0, 0), 80, scene);
            render = new Render(camera, 640, 480);

            MainWindow window = new MainWindow();
            window.Content = render.renderImage;
            window.Show();

            updateThread = new Thread(new ThreadStart(Update));
            updateThread.Start();
        }
        
        void Update()
        {
            while (true)
            {
                Dispatcher.Invoke(render.renderScene);
                Thread.Sleep(10);
            }
        }
    }
}
