using Raytracing.Algebra;

namespace Raytracing
{
    public class Color
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

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

        public static Color operator +(Color a, Color b)
        {
            return new Color((byte)(a.R + b.R), (byte)(a.G + b.G), (byte)(a.B + b.B));
        }

        public override string ToString()
        {
            return "(" + R + ", " + G + ", " + B + ")";
        }
    }
}
