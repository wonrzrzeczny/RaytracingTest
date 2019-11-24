using Raytracing.Algebra;

namespace Raytracing.Lighting
{
    public abstract class Light
    {
        public abstract Vector3 calculateColor(Vector3 point, Vector3 normal);
    }
}
