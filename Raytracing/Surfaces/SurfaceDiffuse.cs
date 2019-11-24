using System;
using Raytracing.Algebra;
using Raytracing.Lighting;

namespace Raytracing.Surfaces
{
    public class SurfaceDiffuse : SurfaceMaterial
    {
        private readonly LightGroup lightGroup;
        private readonly Vector3 color;

        public SurfaceDiffuse(Color color, LightGroup lightGroup)
        {
            this.color = color.toVector();
            this.lightGroup = lightGroup;
        }

        public override Vector3 propagateRay(Ray ray, Vector3 hitPosition, Vector3 normal, int generation)
        {
            return color * lightGroup.calculateColor(hitPosition, normal);
        }
    }
}
