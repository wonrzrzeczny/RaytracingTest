using System;
using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public class SurfaceAbsorptive : SurfaceMaterial
    {
        public override Color propagateRay(Ray ray, Vector3 position, Vector3 normal)
        {
            return new Color(150, 150, 150);
        }
    }
}
