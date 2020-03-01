using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public abstract class SurfaceGeometry
    {
        public Surface Surface { get; set; } = null;

        public abstract CollisionInfo calculateCollision(Ray ray);

        public CollisionInfo noCollision()
        {
            return new CollisionInfo(false);
        }

        public CollisionInfo makeCollision(Vector3 hitPosition, Vector3 normal)
        {
            return new CollisionInfo(true, hitPosition, normal, Surface);
        }
    }
}