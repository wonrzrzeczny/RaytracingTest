using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Algebra
{
    public class Vector3
    {
        public double x, y, z;
        public Vector3()
        {
            x = y = z = 0;
        }

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double magnitude()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public static Vector3 Zero
        {
            get
            {
                return new Vector3(0, 0, 0);
            }
        }

        public static Vector3 Forward
        {
            get
            {
                return new Vector3(0, 0, 1);
            }
        }

        public static Vector3 Right
        {
            get
            {
                return new Vector3(1, 0, 0);
            }
        }

        public static Vector3 Up
        {
            get
            {
                return new Vector3(0, 1, 0);
            }
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 operator *(double d, Vector3 a)
        {
            return new Vector3(d * a.x, d * a.y, d * a.z);
        }

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.x, -v.y, -v.z);
        }

        public static double Dot(Vector3 a, Vector3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        public Vector3 normalized()
        {
            double mag = magnitude();
            return new Vector3(x / mag, y / mag, z / mag);
        }

        public static double Distance(Vector3 a, Vector3 b)
        {
            return (a - b).magnitude();
        }

        public static double ManhattanDistance(Vector3 a, Vector3 b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + Math.Abs(a.z - b.z);
        }
    }
}
