using Raytracing.Algebra;
using System.IO;
using System.Collections.Generic;

namespace Raytracing.Meshes
{
    public static class OBJ
    {
        public static Mesh ParseFile(string filename)
        {
            string[] data = File.ReadAllLines(filename);

            List<Vector3> vertexList = new List<Vector3>();
            List<int[]> faceList = new List<int[]>();

            foreach (string line in data)
            {
                string[] lineData = line.Replace('.', ',').Split(' ');

                if (line.StartsWith("v "))
                {
                    double x = double.Parse(lineData[1]);
                    double y = double.Parse(lineData[2]);
                    double z = double.Parse(lineData[3]);
                    vertexList.Add(new Vector3(x, y, z));
                }

                if (line.StartsWith("f "))
                {
                    int v0 = int.Parse(lineData[1].Split('/')[0]) - 1;
                    for (int i = 3; i < lineData.Length; i++)
                    {
                        int v1 = int.Parse(lineData[i - 1].Split('/')[0]) - 1;
                        int v2 = int.Parse(lineData[i].Split('/')[0]) - 1;
                        faceList.Add(new int[] { v0, v1, v2 });
                    }
                }
            }

            int[,] faces = new int[faceList.Count, 3];
            for (int i = 0; i < faceList.Count; i++)
                for (int j = 0; j < 3; j++)
                    faces[i, j] = faceList[i][j];

            Mesh mesh = new Mesh(vertexList.ToArray(), faces);
            return mesh;
        }
    }
}
