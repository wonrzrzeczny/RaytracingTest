using System;
using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public class SurfaceUnlit : SurfaceMaterial
    {
        private readonly Vector3 color;

        public SurfaceUnlit(Color color)
        {
            this.color = color.toVector();
        }

        public override Vector3 propagateRay(Ray ray, Vector3 hitPosition, Vector3 normal, int generation)
        {
            return color;
        }
    }
}
