using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public abstract class SurfaceGeometry
    {
        public abstract CollisionInfo calculateCollision(Ray ray);
    }
}