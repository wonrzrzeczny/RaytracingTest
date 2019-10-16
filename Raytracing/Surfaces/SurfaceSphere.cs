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
        private double radius;
        private Vector3 center;

        public SurfaceSphere(Vector3 center, double radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            double ox = ray.Origin.x - center.x;
            double oy = ray.Origin.y - center.y;
            double oz = ray.Origin.z - center.z;
            double dx = ray.Direction.x;
            double dy = ray.Direction.y;
            double dz = ray.Direction.z;

            double a = dx * dx + dy * dy + dz * dz;
            double b = 2 * (ox * dx + oy * dy + oz * dz);
            double c = ox * ox + oy * oy + oz * oz - radius * radius;
            double delta = b * b - 4 * a * c;
            if (delta < double.Epsilon)
                return new CollisionInfo(false, null, null);
            double t = Math.Sqrt((-b + Math.Sqrt(delta)) / (2 * a));
            if (t < double.Epsilon)
                return new CollisionInfo(false, null, null);

            return new CollisionInfo(true, ray.Origin + t * ray.Direction, Surface);
        }

        public override Vector3 calculateNormal(Vector3 point)
        {
            return point - center;
        }
    }
}
