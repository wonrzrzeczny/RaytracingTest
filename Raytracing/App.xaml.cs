﻿using Raytracing.Algebra;
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

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            scene = new Scene(new Color(255, 255, 255));
            scene.addSurface(new Surface(new SurfacePlane(-Vector3.Up, Vector3.Forward, Vector3.Right), new SurfaceAbsorptive()));
            scene.addSurface(new Surface(new SurfacePlane(Vector3.Right, Vector3.Forward, Vector3.Up), new SurfaceAbsorptive()));
            camera = new Camera(new Vector3(0, 0, 0), 80, scene);
            render = new Render(camera, 320, 240);

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
