using System;
using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public class SurfaceAbsorptive : SurfaceMaterial
    {
        private readonly Color color;

        public SurfaceAbsorptive(Color color)
        {
            this.color = color;
        }

        public override Color propagateRay(Ray ray, Vector3 hitPosition, Vector3 normal, int generation)
        {
            return color;
        }
    }
}
