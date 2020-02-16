using Raytracing.Algebra;

namespace Raytracing.Lighting
{
    public interface ILightEvaluator
    {
        Vector3 evaluate(Vector3 lightIntensity, Vector3 lightDirection, Vector3 normal);
    }
}
