using Raytracing.Algebra;
using Raytracing.Surfaces;

namespace Raytracing
{
    public class CollisionInfo
    {
        public bool Occured { get; }
        public Vector3 HitPoint { get; }
        public Vector3 Normal { get; }

        public CollisionInfo(bool occured) : this(occured, null, null) { }

        public CollisionInfo(bool occured, Vector3 hitPoint, Vector3 normal)
        {
            Occured = occured;
            HitPoint = hitPoint;
            Normal = normal;
        }
    }
}