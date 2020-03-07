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

        private double shiftMagnitude;
        private Vector3 horizontalShift;
        private Vector3 verticalShift;

        public Camera(Vector3 position, double FOV, int resolutionX, int resolutionY, Scene scene)
        {
            Position = position;
            Direction = Vector3.Forward;
            this.FOV = FOV;
            this.scene = scene;
            this.resolutionX = resolutionX;
            this.resolutionY = resolutionY;

            shiftMagnitude = 2 * Math.Tan(FOV * Math.PI / 360) / resolutionX * Direction.magnitude();
            horizontalShift = -shiftMagnitude * Vector3.Cross(Direction, Vector3.Up).normalized();
            verticalShift = -shiftMagnitude * Vector3.Cross(Direction, horizontalShift).normalized();
        }
        
        public Color renderPixel(int x, int y)
        {
            Vector3 pixelRayDirection = Direction + (x - resolutionX / 2) * horizontalShift + (y - resolutionY / 2) * verticalShift;
            return Color.FromVector(scene.castRay(new Ray(Position, pixelRayDirection), 0));
        }
    }
}
