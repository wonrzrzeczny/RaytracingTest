using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Algebra
{
    public class Matrix4
    {
        double[,] m;
        public Matrix4()
        {
            m = new double[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        }

        public Matrix4(double[,] m)
        {
            this.m = m;
        }

        public static Matrix4 operator +(Matrix4 a, Matrix4 b)
        {
            Matrix4 output = new Matrix4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    output.m[i, j] = a.m[i, j] + b.m[i, j];
                }
            }
            return output;
        }

        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            Matrix4 output = new Matrix4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        output.m[i, j] += a.m[i, k] * b.m[k, j];
                    }
                }
            }
            return output;
        }

        public static Vector3 operator *(Matrix4 m, Vector3 v)
        {
            return new Vector3(m.m[0, 0] * v.x + m.m[0, 1] * v.y + m.m[0, 2] * v.z + m.m[0, 3],
                               m.m[1, 0] * v.x + m.m[1, 1] * v.y + m.m[1, 2] * v.z + m.m[1, 3],
                               m.m[2, 0] * v.x + m.m[2, 1] * v.y + m.m[2, 2] * v.z + m.m[2, 3]);
        }

        public static Matrix4 Translate(double x, double y, double z)
        {
            return new Matrix4(new double[4, 4] { { 1, 0, 0, x }, { 0, 1, 0, y }, { 0, 0, 1, z }, { 0, 0, 0, 1 } });
        }

        public static Matrix4 Translate(Vector3 v)
        {
            return Translate(v.x, v.y, v.z);
        }

        public static Matrix4 Scale(double factor)
        {
            return new Matrix4(new double[4, 4] { { factor, 0, 0, 0 }, { 0, factor, 0, 0 }, { 0, 0, factor, 0 }, { 0, 0, 0, 1 } });
        }

        public static Matrix4 BasicRotationX(double angle)
        {
            double c = Math.Cos(angle);
            double s = Math.Sin(angle);
            return new Matrix4(new double[4, 4] { { 1, 0, 0, 0 }, { 0, c, -s, 0 }, { 0, s, c, 0 }, { 0, 0, 0, 1 } });
        }

        public static Matrix4 BasicRotationY(double angle)
        {
            double c = Math.Cos(angle);
            double s = Math.Sin(angle);
            return new Matrix4(new double[4, 4] { { c, 0, s, 0 }, { 0, 1, 0, 0 }, { -s, 0, c, 0 }, { 0, 0, 0, 1 } });
        }

        public static Matrix4 BasicRotationZ(double angle)
        {
            double c = Math.Cos(angle);
            double s = Math.Sin(angle);
            return new Matrix4(new double[4, 4] { { c, -s, 0, 0 }, { s, c, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } });
        }
        
        // Kinda wonky at the moment
        public static Matrix4 Rotate(double yaw, double pitch, double roll)
        {
            return BasicRotationY(yaw) * BasicRotationX(pitch) * BasicRotationZ(roll);
        }

        public static Matrix4 Rotate(double yaw, double pitch)
        {
            return BasicRotationY(yaw) * BasicRotationX(pitch);
        }

        public static Matrix4 Rotate(Vector3 angles)
        {
            return BasicRotationY(angles.y) * BasicRotationZ(angles.z) * BasicRotationX(angles.x);
        }
    }
}
