using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public abstract class SurfaceGeometry
    {
        public abstract CollisionInfo calculateCollision(Ray ray);
        public abstract Vector3 calculateNormal(Vector3 point);
    }
}