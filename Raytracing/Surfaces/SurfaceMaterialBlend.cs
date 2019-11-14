using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public class SurfaceMaterialBlend : SurfaceMaterial
    {
        private readonly SurfaceMaterial original, additional;
        private readonly Vector3 blend;

        public SurfaceMaterialBlend(SurfaceMaterial original, SurfaceMaterial additional, Vector3 blend)
        {
            this.original = original;
            this.additional = additional;
            this.blend = blend;
        }

        public SurfaceMaterialBlend(SurfaceMaterial original, SurfaceMaterial additional, double blend)
        {
            this.original = original;
            this.additional = additional;
            this.blend = blend * Vector3.One;
        }

        public override Vector3 propagateRay(Ray ray, Vector3 position, Vector3 normal, int generation)
        {
            Vector3 co = original.propagateRay(ray, position, normal, generation);
            Vector3 ca = additional.propagateRay(ray, position, normal, generation);
            return blend * ca + (Vector3.One - blend) * co;
        }
    }
}
