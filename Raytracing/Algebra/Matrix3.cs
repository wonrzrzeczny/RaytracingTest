using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Algebra
{
    public class Matrix3
    {
        double[,] m;
        public Matrix3()
        {
            m = new double[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        }

        public Matrix3(double[,] m)
        {
            this.m = m;
        }

        public static Matrix3 operator +(Matrix3 a, Matrix3 b)
        {
            Matrix3 output = new Matrix3();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    output.m[i, j] = a.m[i, j] + b.m[i, j];
                }
            }
            return output;
        }

        public static Matrix3 operator *(double d, Matrix3 a)
        {
            Matrix3 output = new Matrix3();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    output.m[i, j] = a.m[i, j] * d;
                }
            }
            return output;
        }

        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            Matrix3 output = new Matrix3();
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

        public static Vector3 operator *(Matrix3 m, Vector3 v)
        {
            return new Vector3(m.m[0, 0] * v.x + m.m[1, 0] * v.y + m.m[2, 0] * v.z,
                               m.m[0, 1] * v.x + m.m[1, 1] * v.y + m.m[2, 1] * v.z,
                               m.m[0, 2] * v.x + m.m[1, 2] * v.y + m.m[2, 2] * v.z);
        }

        public double determinant()
        {
            return m[0, 0] * m[1, 1] * m[2, 2] + m[1, 0] * m[2, 1] * m[0, 2] + m[2, 0] * m[0, 1] * m[1, 2]
                 - m[2, 0] * m[1, 1] * m[0, 2] - m[1, 0] * m[0, 1] * m[2, 2] - m[0, 0] * m[2, 1] * m[1, 2];
        }

        public Matrix3 inverse()
        {
            double det = determinant();
            if (Math.Abs(det) < double.Epsilon)
                return new Matrix3();
            double invDet = 1 / determinant();
            return invDet * new Matrix3(new double[3, 3] { 
                { m[1, 1] * m[2, 2] - m[2, 1] * m[1, 2], m[2, 0] * m[1, 2] - m[1, 0] * m[2, 2], m[1, 0] * m[2, 1] - m[2, 0] * m[1, 1] },
                { m[2, 1] * m[0, 2] - m[0, 1] * m[2, 2], m[0, 0] * m[2, 2] - m[2, 0] * m[0, 2], m[2, 0] * m[0, 1] - m[0, 0] * m[2, 1] },
                { m[0, 1] * m[1, 2] - m[1, 1] * m[0, 2], m[1, 0] * m[0, 2] - m[0, 0] * m[1, 2], m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0] } });
        }
    }
}
