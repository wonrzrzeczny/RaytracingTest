using Raytracing.Algebra;
using Raytracing.Lighting;
using Raytracing.Meshes;
using Raytracing.Surfaces;
using Raytracing.TestScenes;
using System.Threading;
using System;
using System.Windows;

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

        public const int resX = 640;
        public const int resY = 480;

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            scene = TestScene.Mesh();

            camera = new Camera(new Vector3(0, 0, 0), 80, resX, resY, scene);
            render = new Render(camera, resX, resY);

            MainWindow window = new MainWindow();
            window.Content = render.RenderImage;
            window.Show();

            //we run renderScene in a new thread, so that we can see pixels' colors as soon as they are calculated 
            Thread thread = new Thread(new ThreadStart(() => render.renderScene()));
            thread.Start();
        }
    }
}
