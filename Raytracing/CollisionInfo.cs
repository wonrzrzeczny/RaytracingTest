using Raytracing.Algebra;
using Raytracing.Surfaces;

namespace Raytracing
{
    public class CollisionInfo
    {
        public bool Occured { get; }
        public Vector3 HitPoint { get; }

        public CollisionInfo(bool occured, Vector3 hitPoint)
        {
            Occured = occured;
            HitPoint = hitPoint;
        }
    }
}