using System;
using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    //Rectangular region parallel to XZ plane with sides parallel to OX and OZ
    //SurfaceFloor is approximately 10 times faster than SurfacePlane,
    //which uses matrix inversion operations, which is extremely slow
    public class SurfaceFloor : SurfaceGeometry
    {
        private readonly double y, x1, x2, z1, z2;

        public SurfaceFloor(double y, double x1, double x2, double z1, double z2)
        {
            this.y = y;
            this.x1 = x1;
            this.x2 = x2;
            this.z1 = z1;
            this.z2 = z2;
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            if (Math.Abs(ray.Direction.y) < double.Epsilon)
                return new CollisionInfo(false, null);
            double t = (y - ray.Origin.y) / ray.Direction.y;
            if (t < double.Epsilon)
                return new CollisionInfo(false, null);

            double x = ray.Origin.x + t * ray.Direction.x;
            if (x < x1 || x > x2)
                return new CollisionInfo(false, null);

            double z = ray.Origin.z + t * ray.Direction.z;
            if (z < z1 || z > z2)
                return new CollisionInfo(false, null);

            return new CollisionInfo(true, new Vector3(x, y, z));
        }

        public override Vector3 calculateNormal(Vector3 point)
        {
            return Vector3.Up;
        }
    }
}
