using System;
using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public class SurfaceTriangle : SurfaceGeometry
    {
        private const double PRECISION = 1e-5;

        private Triangle triangle;

        public SurfaceTriangle(Triangle triangle)
        {
            this.triangle = triangle;
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            double d = Vector3.Dot(triangle.Normal, ray.Direction);
            if (Math.Abs(d) < double.Epsilon)
                return new CollisionInfo(false, null);
            double t = (triangle.OrthDistance - Vector3.Dot(triangle.Normal, ray.Origin)) / d;
            if (t < PRECISION)
                return new CollisionInfo(false, null);

            Vector3 p = ray.Origin + t * ray.Direction;
            Vector3 bar = triangle.toBaricentric(p);
            
            if (bar.x < -PRECISION || bar.y < -PRECISION || bar.z < -PRECISION)
                return new CollisionInfo(false, null);
            return new CollisionInfo(true, p);
        }

        public override Vector3 calculateNormal(Vector3 point)
        {
            return triangle.Normal;
        }
    }
}
