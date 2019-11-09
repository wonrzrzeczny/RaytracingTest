using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public class SurfaceMaterialProduct : SurfaceMaterial
    {
        private readonly SurfaceMaterial original, additional;
        private readonly Vector3 blend;

        public SurfaceMaterialProduct(SurfaceMaterial original, SurfaceMaterial additional, Vector3 blend)
        {
            this.original = original;
            this.additional = additional;
            this.blend = blend;
        }

        public SurfaceMaterialProduct(SurfaceMaterial original, SurfaceMaterial additional, double blend)
        {
            this.original = original;
            this.additional = additional;
            this.blend = blend * Vector3.One;
        }

        public override Color propagateRay(Ray ray, Vector3 position, Vector3 normal, int generation)
        {
            Color co = original.propagateRay(ray, position, normal, generation);
            Color ca = additional.propagateRay(ray, position, normal, generation);
            return blend * ca + (Vector3.One - blend) * co;
        }
    }
}
