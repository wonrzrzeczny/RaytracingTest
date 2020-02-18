namespace Raytracing.Algebra
{
    public class Triangle
    {
        public Vector3 V0 { get; }
        public Vector3 V1 { get; }
        public Vector3 V2 { get; }
        public Vector3 Normal { get; }
        public double OrthDistance { get; }

        private readonly double dot11, dot12, dot22;
        private readonly double det;

        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;

            Normal = Vector3.Cross(v1 - v0, v2 - v0).normalized();
            OrthDistance = Vector3.Dot(Normal, v0);
            dot11 = Vector3.Dot(v1 - v0, v1 - v0);
            dot22 = Vector3.Dot(v2 - v0, v2 - v0);
            dot12 = Vector3.Dot(v1 - v0, v2 - v0);
            det = dot11 * dot22 - dot12 * dot12;
        }

        public Vector3 toBaricentric(Vector3 v)
        {
            double dotp1 = Vector3.Dot(v - V0, V1 - V0);
            double dotp2 = Vector3.Dot(v - V0, V2 - V0);
            double bar1 = (dot22 * dotp1 - dot12 * dotp2) / det;
            double bar2 = (dot11 * dotp2 - dot12 * dotp1) / det;
            double bar3 = 1 - bar1 - bar2;
            return new Vector3(bar1, bar2, bar3);
        }

        public Vector3 fromBaricentric(Vector3 v)
        {
            return v.x * V0 + v.y * V1 + v.z * V2;
        }

        public double lerp(double d0, double d1, double d2, Vector3 p)
        {
            Vector3 bar = toBaricentric(p);
            return bar.x * d0 + bar.y * d1 + bar.z * d2;
        }
    }
}
