using System.Numerics;

public class MatrixElementaryOperations<T> where T : INumber<T>
{
    public static void SwapLines(Matrix<T> m, int p1, int p2)
    {
        T[] cashLine = new T[m.NbColumns];
        for (int i = 0; i < m.NbColumns; i++)
        {
            cashLine[i] = m[p1, i];
            m[p1,i] = m[p2, i];
            m[p2, i] = cashLine[i];
        }
    }

    public static void SwapColumns(Matrix<T> m, int p1, int p2)
    {
        T[] cashLine = new T[m.NbLines];
        for (int i = 0; i < m.NbLines; i++)
        {
            cashLine[i] = m[i, p1];
            m[i,p1] = m[i, p2];
            m[i, p2] = cashLine[i];
        }
    }

    public static void MultiplyLine(Matrix<T> m, int p, T factor)
    {
        if(factor == T.Zero) throw new MatrixScalarZeroException<T>();
        for (int i = 0; i < m.NbColumns; i++)
        {
            m[p, i] *= factor;
        }
    }

    public static void MultiplyColumn(Matrix<T> m, int p, T factor)
    {
        if(factor == T.Zero) throw new MatrixScalarZeroException<T>();
        for (int i = 0; i < m.NbLines; i++)
        {
            m[i, p] *= factor;
        }
    }

    public static void AddLineToAnother(Matrix<T> m, int p1, int p2, T factor)
    {
        for (int i = 0; i < m.NbColumns; i++)
        {
            m[p2,i] += m[p1, i] * factor;
        }
    }

    public static void AddColumnToAnother(Matrix<T> m, int p1, int p2, T factor)
    {
        for (int i = 0; i < m.NbLines; i++)
        {
            m[i,p2] += m[i, p1] * factor;
        }
    }
}