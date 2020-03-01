using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public class SurfaceSphere : SurfaceGeometry
    {
        private readonly double radiusSquared;
        private readonly double invRadius;
        private Vector3 center;

        private const double PRECISION = 1e-5;

        public SurfaceSphere(Vector3 center, double radius)
        {
            this.center = center;
            invRadius = 1 / radius;
            radiusSquared = radius * radius;
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            if ((ray.Origin - center).sqrMagnitude() < radiusSquared)
                return noCollision();

            double ox = ray.Origin.x - center.x;
            double oy = ray.Origin.y - center.y;
            double oz = ray.Origin.z - center.z;
            double dx = ray.Direction.x;
            double dy = ray.Direction.y;
            double dz = ray.Direction.z;

            double a = dx * dx + dy * dy + dz * dz;
            double b = 2 * (ox * dx + oy * dy + oz * dz);
            double c = ox * ox + oy * oy + oz * oz - radiusSquared;
            double delta = b * b - 4 * a * c;
            if (delta < double.Epsilon)
                return noCollision();
            double sdelta = Math.Sqrt(delta);
            double t1 = (-b - sdelta) / (2 * a);
            double t2 = (-b + sdelta) / (2 * a);
            if (t1 > PRECISION)
            {
                Vector3 hitPoint = ray.Origin + t1 * ray.Direction;
                Vector3 normal = invRadius * (hitPoint - center);
                return makeCollision(hitPoint, normal);
            }
            if (t2 > PRECISION)
            {
                Vector3 hitPoint = ray.Origin + t2 * ray.Direction;
                Vector3 normal = invRadius * (hitPoint - center);
                return makeCollision(hitPoint, normal);
            }

            return noCollision();
        }
    }
}
