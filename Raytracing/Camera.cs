using Raytracing.Algebra;
using System;

namespace Raytracing
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }

        private readonly double FOV;
        private readonly int resolutionX;
        private readonly int resolutionY;
        private readonly Scene scene;

        private Vector3[,] pixelRayDirections;

        private void calculateRayDirections()
        {
            pixelRayDirections = new Vector3[resolutionX, resolutionY];
            double FOVvertical = FOV * resolutionY / resolutionX;
            for (int x = 0; x < resolutionX; x++)
            {
                for (int y = 0; y < resolutionY; y++)
                {
                    double angleH = Math.PI / 180 * FOV * (x - resolutionX / 2) / resolutionX;
                    double angleV = Math.PI / 180 * FOVvertical * (y - resolutionY / 2) / resolutionY;
                    pixelRayDirections[x, y] = Matrix4.BasicRotationX(angleV) * Matrix4.BasicRotationY(angleH) * Vector3.Forward;
                }
            }
        }

        public Camera(Vector3 position, double FOV, int resolutionX, int resolutionY, Scene scene)
        {
            Position = position;
            Direction = Vector3.Forward;
            this.FOV = FOV;
            this.scene = scene;
            this.resolutionX = resolutionX;
            this.resolutionY = resolutionY;
            calculateRayDirections();
        }
        
        public Color renderPixel(int x, int y)
        {
            Console.WriteLine("\nCast: " + x.ToString() + " " + y.ToString());
            return scene.castRay(new Ray(Position, pixelRayDirections[x, y]), 0);
        }
    }
}
