using Raytracing.Algebra;
using Raytracing.Lighting;
using Raytracing.Meshes;
using Raytracing.Surfaces;
using Raytracing.TestScenes;
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
        public const int resY = 320;

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            scene = new Scene(new Color(255, 255, 255));

            Vector3[] vertices = new Vector3[] { new Vector3(-1, -1, -1), new Vector3(-1, -1, 1), new Vector3(-1, 1, -1), new Vector3(-1, 1, 1),
                                                 new Vector3(1, -1, -1),  new Vector3(1, -1, 1),  new Vector3(1, 1, -1),  new Vector3(1, 1, 1) };

            int[,] faces = new int[,] { { 0, 1, 4 }, { 5, 4, 1 }, { 6, 3, 2 }, { 3, 6, 7 },
                                        { 0, 4, 2 }, { 6, 2, 4 }, { 3, 5, 1 }, { 5, 3, 7 },
                                        { 0, 2, 1 }, { 3, 1, 2 }, { 5, 6, 4 }, { 6, 5, 7 } };

            Mesh mesh = OBJ.ParseFile(@"..\..\monkey.obj");

            LightGroup lightGroup = new LightGroup();
            lightGroup.addLight(new LightDirectional(new Color(255, 255, 255), new Vector3(0.2, -0.2, 1), scene));

            SurfaceGeometry geometry = new SurfaceMesh(mesh, Vector3.Forward * 10, new Vector3(0, Math.PI, 0));//, Vector3.Zero * (Math.PI / 4));
            SurfaceMaterial material = new SurfaceDiffuse(new Color(255, 255, 255), lightGroup);

            scene.addSurface(new Surface(geometry, material));
            //scene.addSurface(new Surface(new SurfaceSphere(Vector3.Forward * 10, 1), material));

            camera = new Camera(new Vector3(0, 0, 0), 80, resX, resY, scene);
            render = new Render(camera, resX, resY);

            MainWindow window = new MainWindow();
            window.Content = render.RenderImage;
            window.Show();

            render.renderScene();
        }
    }
}
