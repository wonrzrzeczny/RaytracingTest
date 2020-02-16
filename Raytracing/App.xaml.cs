﻿using Raytracing.Algebra;
using Raytracing.Surfaces;
using Raytracing.Surfaces.Mesh;
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
            scene = new Scene(new Color(255, 255, 255));

            Vector3[] vertices = new Vector3[] { new Vector3(-1, -1, -1), new Vector3(-1, -1, 1), new Vector3(-1, 1, -1), new Vector3(-1, 1, 1),
                                                 new Vector3(1, -1, -1),  new Vector3(1, -1, 1),  new Vector3(1, 1, -1),  new Vector3(1, 1, 1) };

            int[,] faces = new int[,] { { 0, 1, 4 }, { 5, 4, 1 }, { 6, 3, 2 }, { 3, 6, 7 },
                                        { 0, 4, 2 }, { 6, 2, 4 }, { 3, 5, 1 }, { 5, 3, 7 },
                                        { 0, 2, 1 }, { 3, 2, 1 }, { 5, 6, 4 }, { 5, 6, 7 } };

            SurfaceGeometry mesh = new SurfaceMesh(vertices, faces, Vector3.Forward * 10);
            SurfaceMaterial material = new SurfaceUnlit(new Color(125, 125, 125));

            scene.addSurface(new Surface(mesh, material));
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
