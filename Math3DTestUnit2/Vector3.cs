using System.Numerics;

namespace Maths_Matrices.Tests;

public class Vector3<T> where T : INumber<T>
{
    public T x;
    public T y;
    public T z;

    public Vector3(T m_x, T m_y, T m_z)
    {
        x = m_x;
        y = m_y;
        z = m_z;
    }

    public Vector3<T> Normalize()
    {
        T magnitude = T.CreateChecked(Math.Sqrt(double.CreateChecked(x * x + y * y + z * z)));
        return new Vector3<T>(x / magnitude, y / magnitude, z / magnitude);
    }

    public static Vector3<T> operator+(Vector3<T> a, Vector3<T> b)
    {
        return new Vector3<T>(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    
    public static Vector3<T> operator%(Vector3<T> a, T b)
    {
        return new Vector3<T>(a.x % b, a.y % b, a.z % b);
    }
    
    public static implicit operator Vector3<T>(Vector4<T> a)
    {
        return new Vector3<T>(a.x, a.y, a.z);
    }
}