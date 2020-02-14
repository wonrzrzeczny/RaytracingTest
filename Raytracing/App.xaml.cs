using Raytracing.Algebra;
using Raytracing.TestScenes;
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

        public const int resX = 1280;
        public const int resY = 720;

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            scene = TestScene.Boolean(false);
            camera = new Camera(new Vector3(0, 0, 0), 80, resX, resY, scene);
            render = new Render(camera, resX, resY);

            MainWindow window = new MainWindow();
            window.Content = render.RenderImage;
            window.Show();

            render.renderScene();
        }
    }
}
