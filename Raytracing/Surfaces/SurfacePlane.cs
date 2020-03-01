using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public class SurfacePlane : SurfaceGeometry
    {
        private const double PRECISION = 1e-5;

        private readonly Vector3 origin;
        private readonly Vector3 spanA;
        private readonly Vector3 spanB;
        private readonly Vector3 normal;

        private readonly double orthDistance;

        public SurfacePlane(Vector3 origin, Vector3 spanA, Vector3 spanB)
        {
            this.origin = origin;
            this.spanA = spanA;
            this.spanB = spanB;
            normal = Vector3.Cross(spanA, spanB).normalized();
            orthDistance = Vector3.Dot(normal, origin);
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            double d = Vector3.Dot(normal, ray.Direction);
            if (Math.Abs(d) < double.Epsilon)
                return noCollision();
            double t = (orthDistance - Vector3.Dot(normal, ray.Origin)) / d;
            if (t < PRECISION)
                return noCollision();

            return makeCollision(ray.Origin + t * ray.Direction, normal);
        }
    }
}
