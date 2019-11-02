using Raytracing.Algebra;
using System;

namespace Raytracing.Surfaces
{
    public class SurfaceReflective : SurfaceMaterial
    {
        private readonly Scene scene;
        
        public SurfaceReflective(Scene scene)
        {
            this.scene = scene;
        }

        public override Color propagateRay(Ray ray, Vector3 hitPosition, Vector3 normal, int generation)
        {
            Console.WriteLine("Reflective surface hit, position: " + hitPosition);
            return scene.castRay(new Ray(hitPosition, normal.reflect(-ray.Direction)), generation);
        }
    }
}
