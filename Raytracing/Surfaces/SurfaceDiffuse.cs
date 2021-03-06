﻿using Raytracing.Algebra;
using Raytracing.Lighting;
using System;

namespace Raytracing.Surfaces
{
    public class SurfaceDiffuse : SurfaceMaterial, ILightEvaluator
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
            return lightGroup.calculateColor(hitPosition, normal, this);
        }


        Vector3 ILightEvaluator.evaluate(Vector3 lightIntensity, Vector3 lightDirection, Vector3 normal)
        {
            return Math.Max(0, Vector3.Dot(normal, -lightDirection)) * lightIntensity * color;
        }
    }
}
