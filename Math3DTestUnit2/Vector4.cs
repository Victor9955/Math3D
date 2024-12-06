using System.Numerics;

namespace Maths_Matrices.Tests;

public class Vector4<T> where T : INumber<T>
{
    public T x;
    public T y;
    public T z;
    public T w;

    public Vector4(T m_x, T m_y, T m_z, T m_w)
    {
        x = m_x;
        y = m_y;
        z = m_z;
        w = m_w;
    }
    
    public static Vector4<T> operator *(Matrix<T> m, Vector4<T> v)
    {
        return new Vector4<T>(
            m[0, 0] * v.x + m[0, 1] * v.y + m[0, 2] * v.z + m[0, 3] * v.w,
            m[1, 0] * v.x + m[1, 1] * v.y + m[1, 2] * v.z + m[1, 3] * v.w,
            m[2, 0] * v.x + m[2, 1] * v.y + m[2, 2] * v.z + m[2, 3] * v.w,
            m[3, 0] * v.x + m[3, 1] * v.y + m[3, 2] * v.z + m[3, 3] * v.w
        );
    }

}