using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public abstract class SurfaceGeometry
    {
        public Surface Surface { get; set; }

        public abstract CollisionInfo calculateCollision(Ray ray);
        public abstract Vector3 calculateNormal(Vector3 point);
    }
}