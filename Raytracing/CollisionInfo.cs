using Raytracing.Algebra;
using Raytracing.Surfaces;

namespace Raytracing
{
    public class CollisionInfo
    {
        public bool Occured { get; }
        public Vector3 HitPoint { get; }
        public Vector3 Normal { get; }
        public Surface Surface { get; }

        public CollisionInfo(bool occured) : this(occured, null, null, null) { }

        public CollisionInfo(bool occured, Vector3 hitPoint, Vector3 normal, Surface surface)
        {
            Occured = occured;
            HitPoint = hitPoint;
            Normal = normal;
            Surface = surface;
        }
    }
}