using Raytracing.Algebra;

namespace Raytracing.Meshes
{
    public class Mesh
    {
        public int VertexCount { get; }
        public int FaceCount { get; }

        private readonly Vector3[] vertices;
        private readonly int[,] faces;

        public Mesh(Vector3[] vertices, int[,] faces)
        {
            this.vertices = vertices;
            this.faces = faces;

            VertexCount = vertices.GetLength(0);
            FaceCount = faces.GetLength(0);
        }

        public Vector3[] getVertices()
        {
            return (Vector3[])vertices.Clone();
        }

        public int[,] getFaces()
        {
            return (int[,])faces.Clone();
        }
    }
}
