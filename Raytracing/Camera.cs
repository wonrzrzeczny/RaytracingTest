using Raytracing.Algebra;

namespace Raytracing
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }

        private readonly double FOV;
        private readonly Scene scene;

        public Camera(Vector3 position, double FOV, Scene scene)
        {
            Position = position;
            Direction = Vector3.Forward;
            this.FOV = FOV;
            this.scene = scene;
        }
        
        public Color renderPixel(int x, int y)
        {
            return scene.castRay(new Ray(Position, new Vector3(x * 0.02d - 3.2d, y * (-0.02d) + 2.4d, 5d)));
        }
    }
}
