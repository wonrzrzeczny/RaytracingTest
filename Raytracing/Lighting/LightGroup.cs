﻿using Raytracing.Algebra;
using System.Collections.Generic;

namespace Raytracing.Lighting
{
    public class LightGroup
    {
        private readonly List<Light> lights;

        public LightGroup()
        {
            lights = new List<Light>();
        }

        public LightGroup(List<Light> lights)
        {
            this.lights = lights;
        }

        public void addLight(Light light)
        {
            lights.Add(light);
        }

        public Vector3 calculateColor(Vector3 point, Vector3 normal)
        {
            Vector3 result = Vector3.Zero;
            foreach (Light light in lights)
                result += light.calculateColor(point, normal);
            return result;
        }
    }
}
