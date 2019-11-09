﻿using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public abstract class SurfaceMaterial
    {
        public abstract Color propagateRay(Ray ray, Vector3 hitPosition, Vector3 normal, int generation);
    }
}