using System;
using Raytracing.Algebra;
using Raytracing.Meshes;

namespace Raytracing.Surfaces
{
    public class SurfaceMesh : SurfaceGeometry
    {
        public enum NormalMode
        {
            PER_FACE,
            PER_VERTEX
        }

        private const double PRECISION = 1e-5;

        private readonly Vector3[] vertices;
        private readonly NormalMode normalMode;
        private readonly Vector3[] vertexNormals;
        private readonly int[,] faces;
        private readonly Triangle[] triangles;

        public SurfaceMesh(Mesh mesh) : this(mesh, Vector3.Zero, Vector3.Zero, NormalMode.PER_FACE) { }
        public SurfaceMesh(Mesh mesh, Vector3 position) : this(mesh, position, Vector3.Zero, NormalMode.PER_FACE) { }
        public SurfaceMesh(Mesh mesh, Vector3 position, Vector3 rotation) : this(mesh, position, rotation, NormalMode.PER_FACE) { }

        public SurfaceMesh(Mesh mesh, Vector3 position, Vector3 rotation, NormalMode normalMode)
        {
            this.normalMode = normalMode;

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

            if (normalMode == NormalMode.PER_VERTEX)
            {
                vertexNormals = new Vector3[vertices.Length];
                for (int i = 0; i < vertices.Length; i++)
                {
                    vertexNormals[i] = Vector3.Zero;
                }
                for (int i = 0; i < triangles.Length; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        vertexNormals[faces[i, j]] = vertexNormals[faces[i, j]] + triangles[i].Normal;
                    }
                }
                for (int i = 0; i < vertices.Length; i++)
                {
                    vertexNormals[i] = vertexNormals[i].normalized();
                }
            }
            else
            {
                vertexNormals = null;
            }
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            double t = double.MinValue;
            int face = -1;
            Vector3 hitPoint = null;
            Vector3 hitPointBaricentric = null;

            for (int i = 0; i < triangles.Length; i++)
            {
                Triangle triangle = triangles[i];
                double d = Vector3.Dot(triangle.Normal, ray.Direction);
                if (Math.Abs(d) > double.Epsilon)
                {
                    double tt = (triangle.OrthDistance - Vector3.Dot(triangle.Normal, ray.Origin)) / d;

                    if (tt > PRECISION && (t < PRECISION || tt < t))
                    {
                        Vector3 p = ray.Origin + tt * ray.Direction;
                        Vector3 bar = triangle.toBaricentric(p);

                        if (bar.x > -PRECISION && bar.y > -PRECISION && bar.z > -PRECISION)
                        {
                            t = tt;
                            face = i;
                            hitPoint = p;
                            hitPointBaricentric = bar;
                        }
                    }
                }
            }

            if (t < PRECISION)
                return new CollisionInfo(false);

            Vector3 hitNormal;
            if (normalMode == NormalMode.PER_FACE)
            {
                hitNormal = triangles[face].Normal;
            }
            else
            {
                Vector3 n0 = vertexNormals[faces[face, 0]];
                Vector3 n1 = vertexNormals[faces[face, 1]];
                Vector3 n2 = vertexNormals[faces[face, 2]];
                hitNormal = hitPointBaricentric.x * n0 + hitPointBaricentric.y * n1 + hitPointBaricentric.z * n2;
                hitNormal = hitNormal.normalized();
            }
            return new CollisionInfo(true, hitPoint, hitNormal);
        }
    }
}
