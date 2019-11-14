using System;
using System.Collections.Generic;
using Raytracing.Algebra;
using Raytracing.Surfaces;

namespace Raytracing
{
    public class Scene
    {
        public const int MAX_GENERATIONS = 8;

        private readonly List<Surface> surfaces;
        private readonly Vector3 skyColor;


        public Scene(Color skyColor)
        {
            this.skyColor = skyColor.toVector();
            surfaces = new List<Surface>();
        }

        public void addSurface(Surface surface)
        {
            surfaces.Add(surface);
        }


        public Vector3 castRay(Ray ray, int generation)
        {
            //Console.WriteLine("Generation " + generation.ToString() + " Ray: " + ray.ToString());

            CollisionInfo firstCollision = null;
            Surface hitSurface = null;
            double distance = double.PositiveInfinity;
            foreach (Surface surface in surfaces)
            {
                CollisionInfo collision = surface.Geometry.calculateCollision(ray);
                if (collision.Occured)
                {
                    double newDistance = Vector3.ManhattanDistance(ray.Origin, collision.HitPoint);
                    if (distance > newDistance)
                    {
                        distance = newDistance;
                        firstCollision = collision;
                        hitSurface = surface;
                    }
                }
            }

            if (firstCollision == null)
                return skyColor;

            if (generation > MAX_GENERATIONS)
                return skyColor;
            return hitSurface.Material.propagateRay(
                ray, firstCollision.HitPoint, hitSurface.Geometry.calculateNormal(firstCollision.HitPoint), generation + 1);
        }
    }
}
