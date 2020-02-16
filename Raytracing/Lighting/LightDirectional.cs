using Raytracing.Algebra;

namespace Raytracing.Lighting
{
    public class LightDirectional : Light
    {
        private const double SKIP = 1e-5;

        private readonly Vector3 direction;
        private readonly Vector3 color;
        private readonly Scene scene;
        private readonly bool castShadows;

        public LightDirectional(Color color, Vector3 direction, Scene scene, bool castShadows)
        {
            this.direction = direction.normalized();
            this.color = color.toVector();
            this.scene = scene;
            this.castShadows = castShadows;
        }

        public LightDirectional(Color color, Vector3 direction, Scene scene) : this(color, direction, scene, false) { }

        public override Vector3 calculateIntensity(Vector3 point)
        {
            double visibility = castShadows ? (scene.checkSkyVisibility(new Ray(point + SKIP * (-direction), -direction)) ? 1 : 0) : 1;
            return visibility * color;
        }

        public override Vector3 calculateLightDirection(Vector3 point)
        {
            return direction;
        }
    }
}
