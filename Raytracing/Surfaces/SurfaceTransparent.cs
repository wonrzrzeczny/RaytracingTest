using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public class SurfaceTransparent : SurfaceMaterial
    {
        private const double SKIP = 1e-4;
        private readonly Scene scene;

        public SurfaceTransparent(Scene scene)
        {
            this.scene = scene;
        }

        public override Color propagateRay(Ray ray, Vector3 hitPosition, Vector3 normal, int generation)
        {
            return scene.castRay(new Ray(hitPosition + SKIP * ray.Direction.normalized(), ray.Direction), generation);
        }
    }
}
