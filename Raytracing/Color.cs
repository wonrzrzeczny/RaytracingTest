using Raytracing.Algebra;

namespace Raytracing
{
    public class Color
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static Color operator *(Vector3 a, Color b)
        {
            return new Color((byte)(a.x * b.R), (byte)(a.y * b.G), (byte)(a.z * b.B));
        }
    }
}
