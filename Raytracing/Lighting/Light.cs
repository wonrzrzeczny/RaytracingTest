using Raytracing.Algebra;

namespace Raytracing.Lighting
{
    public abstract class Light
    {
        public abstract Vector3 calculateIntensity(Vector3 point);

        public abstract Vector3 calculateLightDirection(Vector3 point);
    }
}
