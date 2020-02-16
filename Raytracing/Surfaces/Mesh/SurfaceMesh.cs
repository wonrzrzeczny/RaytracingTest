using System;
using System.Collections.Generic;
using Raytracing.Algebra;

namespace Raytracing.Surfaces.Mesh
{
    public class SurfaceMesh : SurfaceGeometry
    {
        private const double PRECISION = 1e-5;

        private readonly Vector3[] vertices;
        private readonly int[,] faces;
        private readonly Triangle[] triangles;

        public SurfaceMesh(Vector3[] vertices, int[,] faces)
        {
            this.vertices = vertices;
            this.faces = faces;
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            throw new NotImplementedException();
        }

        public override Vector3 calculateNormal(Vector3 point)
        {
            throw new NotImplementedException();
        }
    }
}
