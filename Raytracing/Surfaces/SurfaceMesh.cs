using System;
using System.Collections.Generic;
using Raytracing.Algebra;
using Raytracing.Meshes;

namespace Raytracing.Surfaces
{
    public class SurfaceMesh : SurfaceGeometry
    {
        private const double PRECISION = 1e-5;

        private readonly Vector3[] vertices;
        private readonly int[,] faces;
        private readonly Triangle[] triangles;

        public SurfaceMesh(Mesh mesh) : this(mesh, Vector3.Zero, Vector3.Zero) { }

        public SurfaceMesh(Mesh mesh, Vector3 position) : this(mesh, position, Vector3.Zero) { }

        public SurfaceMesh(Mesh mesh, Vector3 position, Vector3 rotation)
        {
            vertices = mesh.getVertices();
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = Matrix4.Translate(position) * Matrix4.Rotate(rotation) * vertices[i];
            }

            faces = mesh.getFaces();

            triangles = new Triangle[faces.GetLength(0)];
            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i] = new Triangle(vertices[faces[i, 0]], vertices[faces[i, 1]], vertices[faces[i, 2]]);
            }
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            double t = double.MinValue;
            Triangle face = null;
            Vector3 hitPoint = null;
            Vector3 hitPointBaricentric = null;

            foreach (Triangle triangle in triangles)
            {
                double d = Vector3.Dot(triangle.Normal, ray.Direction);
                if (Math.Abs(d) > double.Epsilon)
                {
                    double tt = (triangle.OrthDistance - Vector3.Dot(triangle.Normal, ray.Origin)) / d;

                    if (tt > PRECISION && (t < PRECISION || tt < t))
                    {
                        Vector3 p = ray.Origin + tt * ray.Direction;
                        Vector3 bar = triangle.toBaricentric(p);

                        if (bar.x >= 0 && bar.y >= 0 && bar.z >= 0)
                        {
                            t = tt;
                            face = triangle;
                            hitPoint = p;
                            hitPointBaricentric = bar;
                        }
                    }
                }
            }

            if (t < PRECISION)
                return new CollisionInfo(false);
            return new CollisionInfo(true, hitPoint, face.Normal);
        }
    }
}
