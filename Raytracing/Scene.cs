using System;
using System.Collections.Generic;
using Raytracing.Algebra;
using Raytracing.Surfaces;

namespace Raytracing
{
    public class Scene
    {
        private readonly List<Surface> surfaces;
        private readonly Color skyColor;


        public Scene(Color skyColor)
        {
            this.skyColor = skyColor;
            surfaces = new List<Surface>();
        }

        public void addSurface(Surface surface)
        {
            surfaces.Add(surface);
        }


        public Color castRay(Ray ray)
        {
            CollisionInfo firstCollision = null;
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
                    }
                }
            }

            if (firstCollision == null)
                return skyColor;

            return firstCollision.HitSurface.Material.propagateRay(
                ray, firstCollision.HitPoint, firstCollision.HitSurface.Geometry.calculateNormal(firstCollision.HitPoint));
        }
    }
}
