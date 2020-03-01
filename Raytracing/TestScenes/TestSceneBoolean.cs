using Raytracing.Algebra;
using Raytracing.Lighting;
using Raytracing.Surfaces;
using System;
using System.Collections.Generic;

namespace Raytracing.TestScenes
{
    class VolumeSphere
    {
        private readonly double radiusSquared;
        private readonly double invRadius;
        private Vector3 center;

        private const double PRECISION = 1e-5;

        public VolumeSphere(Vector3 center, double radius)
        {
            this.center = center;
            invRadius = 1 / radius;
            radiusSquared = radius * radius;
        }

        public List<double> calculateCollision(Ray ray)
        {
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
                return new List<double>();
            double sdelta = Math.Sqrt(delta);
            List<double> ret = new List<double>();
            ret.Add((-b - sdelta) / (2*a));
            ret.Add((-b + sdelta) / (2*a));
            return ret;
        }

        public Vector3 calculateNormal(Vector3 point)
        {
            return invRadius * (point - center);
        }

        public bool isInside(Vector3 point)
        {
            return radiusSquared >= (point - center).sqrMagnitude();
        }

        public bool isOnSurface(Vector3 point)
        {
            return Math.Abs(radiusSquared - (point - center).sqrMagnitude()) < PRECISION;
        }
    }

    class SurfaceIntersection : SurfaceGeometry
    {
        private VolumeSphere s1, s2;

        public SurfaceIntersection(VolumeSphere s1, VolumeSphere s2)
        {
            this.s1 = s1;
            this.s2 = s2;
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            List<double> col1 = s1.calculateCollision(ray);
            List<double> col2 = s2.calculateCollision(ray);

            if (col1.Count == 0 || col2.Count == 0)
                return noCollision();

            if (col1[1] < col2[0] || col2[1] < col1[0])
                return noCollision();

            double t = Math.Max(col1[0], col2[0]);
            if (t > 0)
            {
                Vector3 hitPosition = ray.Origin + t * ray.Direction;
                Vector3 normal = s1.isOnSurface(hitPosition) ? s1.calculateNormal(hitPosition) : s2.calculateNormal(hitPosition);
                return makeCollision(hitPosition, normal);
            }

            return noCollision();
        }
    }

    class SurfaceDifference : SurfaceGeometry
    {
        private VolumeSphere s1, s2;

        public SurfaceDifference(VolumeSphere s1, VolumeSphere s2)
        {
            this.s1 = s1;
            this.s2 = s2;
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            List<double> col1 = s1.calculateCollision(ray);
            List<double> col2 = s2.calculateCollision(ray);

            if (col1.Count == 0)
                return noCollision();

            double t = double.MinValue;
            if (col2.Count == 0)
            {
                t = col1[0];
            }
            else
            {
                if (col1[0] > 0 && (col1[0] < col2[0] || col1[0] > col2[1]))
                    t = col1[0];
                else if (col2[1] > 0 && col2[1] > col1[0] && col2[1] < col1[1])
                    t = col2[1];
            }

            if (t > 0)
            {
                Vector3 hitPosition = ray.Origin + t * ray.Direction;
                Vector3 normal = s1.isOnSurface(hitPosition) ? s1.calculateNormal(hitPosition) : s2.calculateNormal(hitPosition);
                return makeCollision(hitPosition, normal);
            }

            return noCollision();
        }
    }

    public static partial class TestScene
    {
        public static Scene Boolean(bool outlines)
        {
            Scene scene = new Scene(new Color(255, 255, 255));

            Light sun = new LightDirectional(new Color(255, 255, 255), new Vector3(0, -1, 1), scene, false);
            LightGroup lightGroup = new LightGroup();
            lightGroup.addLight(sun);

            Vector3 center1 = new Vector3(-12, 0, 30);
            Vector3 center2 = new Vector3(-6, 0, 30);
            Vector3 center3 = new Vector3(9, 0, 30);
            Vector3 center4 = new Vector3(9, 1, 27);
            double radius = 5;
            double radius2 = 3;

            SurfaceGeometry surfaceIntersection = new SurfaceIntersection(new VolumeSphere(center1, radius), new VolumeSphere(center2, radius));
            SurfaceGeometry surfaceDifference = new SurfaceDifference(new VolumeSphere(center3, radius), new VolumeSphere(center4, radius2));

            SurfaceMaterial diffuse = new SurfaceDiffuse(new Color(255, 255, 255), lightGroup);
            SurfaceMaterial semiopaque = new SurfaceMaterialBlend(new SurfaceTransparent(scene), diffuse, 0.2);

            scene.addSurface(new Surface(surfaceIntersection, diffuse));
            scene.addSurface(new Surface(surfaceDifference, diffuse));
            if (outlines)
            {
                scene.addSurface(new Surface(new SurfaceSphere(center1, radius), semiopaque));
                scene.addSurface(new Surface(new SurfaceSphere(center2, radius), semiopaque));
                scene.addSurface(new Surface(new SurfaceSphere(center3, radius), semiopaque));
                scene.addSurface(new Surface(new SurfaceSphere(center4, radius2), semiopaque));
            }

            return scene;
        }
    }
}
