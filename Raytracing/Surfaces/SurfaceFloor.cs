﻿using System;
using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    //Rectangular region parallel to XZ plane with sides parallel to OX and OZ
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
                return noCollision();
            double t = (y - ray.Origin.y) / ray.Direction.y;
            if (t < double.Epsilon)
                return noCollision();

            double x = ray.Origin.x + t * ray.Direction.x;
            if (x < x1 || x > x2)
                return noCollision();

            double z = ray.Origin.z + t * ray.Direction.z;
            if (z < z1 || z > z2)
                return noCollision();

            return makeCollision(new Vector3(x, y, z), Vector3.Up);
        }
    }
}
