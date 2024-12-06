using System;
using System.Numerics;

namespace Maths_Matrices.Tests
{
    public struct Quaternion
    {
        public float x, y, z, w;
        public Matrix<float> Matrix
        {
            get
            {
                return new Matrix<float>(
                    new float[,]
                    {
                        {1 - 2 * (y * y + z * z), 2 * (x * y - z * w),     2 * (x * z + y * w),     0f},
                        {2 * (x * y + z * w),     1 - 2 * (x * x + z * z), 2 * (y * z - x * w),     0f},
                        {2 * (x * z - y * w),     2 * (y * z + x * w),     1 - 2 * (x * x + y * y), 0f},
                        {0f,                      0f,                      0f,                      1f}
                    });
            }
        }

        public Vector3<float> EulerAngles
        {
            get
            {
                Vector3<float> euler = new Vector3<float>(0, 0, 0);
                float unit = x * x + y * y + z * z + w * w;

                float test = x * w - y * z;

                if (test > 0.4995f * unit)
                {
                    euler.x = float.Pi / 2.0f;
                    euler.y = 2f * float.Atan2(y, x);
                    euler.z = 0f;
                }
                else if (test < -0.4995f * unit)
                {
                    euler.x = -float.Pi / 2.0f;
                    euler.y = -2f * float.Atan2(y, x);
                    euler.z = 0f;
                }
                else
                {
                    euler.x = float.Asin(2f * (w * x - y * z));
                    euler.y = float.Atan2(2f * w * y + 2f * z * x,1f - 2f * (x * x + y * y));
                    euler.z = float.Atan2(2f * w * z + 2f * x * y, 1f - 2f * (z * z + x * x));
                }
                
                euler = new Vector3<float>(float.RadiansToDegrees(euler.x), float.RadiansToDegrees(euler.y), float.RadiansToDegrees(euler.z));
                
                return euler;
            }
        }





        public Quaternion(float m_x, float m_y, float m_z, float m_w)
        {
            x = m_x;
            y = m_y;
            z = m_z;
            w = m_w;
        }

        public static Quaternion Identity => new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);

        public static Quaternion AngleAxis(float angle, Vector3<float> axis)
        {
            Vector3<float> result = axis.Normalize();
            float halfAngle = float.DegreesToRadians(angle) / 2f;
            float sinHalfAngle = float.Sin(halfAngle);
            return new Quaternion(
                result.x * sinHalfAngle,
                result.y * sinHalfAngle,
                result.z * sinHalfAngle,
                float.Cos(halfAngle)
            );
        }

        public static Vector3<float> Rotate(Quaternion q, Vector3<float> v)
        {
            Quaternion qv = new Quaternion(v.x, v.y, v.z, 0);
            Quaternion qConjugate = new Quaternion(-q.x, -q.y, -q.z, q.w);

            Quaternion result = q * qv * qConjugate;
            return new Vector3<float>(result.x, result.y, result.z);
        }

        public static Quaternion operator *(Quaternion q, Quaternion q2)
        {
            Quaternion result = new Quaternion();
            result.w = q.w * q2.w - q.x * q2.x - q.y * q2.y - q.z * q2.z;
            result.x = q.w * q2.x + q.x * q2.w + q.y * q2.z - q.z * q2.y;
            result.y = q.w * q2.y - q.x * q2.z + q.y * q2.w + q.z * q2.x;
            result.z = q.w * q2.z + q.x * q2.y - q.y * q2.x + q.z * q2.w;
            return result;
        }

        public static Vector3<float> operator *(Quaternion q, Vector3<float> v)
        {
            return Rotate(q, v);
        }

        public static Quaternion Euler(float x, float y, float z)
        {
            return AngleAxis(y, new Vector3<float>(0, 1, 0)) * AngleAxis(x, new Vector3<float>(1, 0, 0)) * AngleAxis(z, new Vector3<float>(0, 0, 1));
        }
    }
}
