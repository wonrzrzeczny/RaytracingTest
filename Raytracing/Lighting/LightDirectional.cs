using Raytracing.Algebra;
using System;

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

        public LightDirectional(Color color, Vector3 direction, Scene scene) : this(color, direction, scene, true) { }

        public override Vector3 calculateColor(Vector3 point, Vector3 normal)
        {
            double visibility = castShadows ? (scene.checkSkyVisibility(new Ray(point + SKIP * (-direction), -direction)) ? 1 : 0) : 1;
            return visibility * Math.Max(0, Vector3.Dot(normal, -direction)) * color;
        }
    }
}
