using Raytracing.Algebra;
using Raytracing.Surfaces;

namespace Raytracing
{
    public class CollisionInfo
    {
        public bool Occured { get; }
        public Vector3 HitPoint { get; }
        public Surface HitSurface { get; }

        public CollisionInfo(bool occured, Vector3 hitPoint, Surface hitSurface)
        {
            Occured = occured;
            HitPoint = hitPoint;
            HitSurface = hitSurface;
        }
    }
}