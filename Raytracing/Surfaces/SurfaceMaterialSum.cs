using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public class SurfaceMaterialSum : SurfaceMaterial
    {
        private readonly SurfaceMaterial materialA, materialB;

        public SurfaceMaterialSum(SurfaceMaterial materialA, SurfaceMaterial materialB)
        {
            this.materialA = materialA;
            this.materialB = materialB;
        }

        public override Vector3 propagateRay(Ray ray, Vector3 hitPosition, Vector3 normal, int generation)
        {
            return materialA.propagateRay(ray, hitPosition, normal, generation) + materialB.propagateRay(ray, hitPosition, normal, generation);
        }
    }
}
