using Raytracing.Algebra;
using Raytracing.Lighting;
using System;

namespace Raytracing.Surfaces
{
    public class SurfaceSpecular : SurfaceMaterial, ILightEvaluator
    {
        private readonly LightGroup lightGroup;
        private readonly Vector3 color;
        private readonly double exponent;

        public SurfaceSpecular(Color color, double exponent, LightGroup lightGroup)
        {
            this.color = color.toVector();
            this.lightGroup = lightGroup;
            this.exponent = exponent;
        }

        public override Vector3 propagateRay(Ray ray, Vector3 hitPosition, Vector3 normal, int generation)
        {
            return lightGroup.calculateColor(hitPosition, normal, this);
        }


        Vector3 ILightEvaluator.evaluate(Vector3 lightIntensity, Vector3 lightDirection, Vector3 normal)
        {
            if (Vector3.Dot(normal, -lightDirection) < 0)
                return Vector3.Zero;

            Vector3 halfwayVector = (-lightDirection + normal).normalized();
            return Math.Pow(Math.Max(0, Vector3.Dot(normal, halfwayVector)), exponent) * lightIntensity * color;
        }
    }
}
