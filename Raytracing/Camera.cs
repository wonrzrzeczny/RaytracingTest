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
            double shiftMagnitude = 2 * Math.Tan(FOV * Math.PI / 360) / resolutionX * Direction.magnitude();
            Vector3 horizontalShift = - shiftMagnitude * Vector3.Cross(Direction, Vector3.Up).normalized();
            Vector3 verticalShift = - shiftMagnitude * Vector3.Cross(Direction, horizontalShift).normalized();
            for (int x = 0; x < resolutionX; x++)
            {
                for (int y = 0; y < resolutionY; y++)
                {
                    pixelRayDirections[x, y] = Direction + (x - resolutionX / 2) * horizontalShift + (y - resolutionY / 2) * verticalShift;
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
            //Console.WriteLine("\nCast: " + x.ToString() + " " + y.ToString());
            return Color.fromVector(scene.castRay(new Ray(Position, pixelRayDirections[x, y]), 0));
        }
    }
}
