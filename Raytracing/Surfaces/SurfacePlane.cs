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
        private readonly Vector3 origin;
        private readonly Vector3 spanA;
        private readonly Vector3 spanB;
        private readonly Vector3 normal;

        public SurfacePlane(Vector3 origin, Vector3 spanA, Vector3 spanB)
        {
            this.origin = origin;
            this.spanA = spanA;
            this.spanB = spanB;
            normal = Vector3.Cross(spanA, spanB).normalized();
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            Matrix3 matrix = new Matrix3(new double[3, 3] { { ray.Direction.x, spanA.x, spanB.x }, 
                                                            { ray.Direction.y, spanA.y, spanB.y }, 
                                                            { ray.Direction.z, spanA.z, spanB.z } });

            double t = (matrix.inverse() * (origin - ray.Origin)).x;
            if (t < double.Epsilon)
                return new CollisionInfo(false, null);

            return new CollisionInfo(true, ray.Origin + t * ray.Direction);
        }

        public override Vector3 calculateNormal(Vector3 point)
        {
            return normal;
        }
    }
}
