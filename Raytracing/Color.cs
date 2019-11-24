using Raytracing.Algebra;
using System;

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

        public Vector3 toVector()
        {
            return new Vector3((float)R / 255, (float)G / 255, (float)B / 255);
        }

        public static Color FromVector(Vector3 v)
        {
            return new Color((byte)Math.Min(255, v.x * 255), (byte)Math.Min(255, v.y * 255), (byte)Math.Min(255, v.z * 255));
        }

        public override string ToString()
        {
            return "(" + R + ", " + G + ", " + B + ")";
        }
    }
}
