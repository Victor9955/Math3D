using System.Numerics;

namespace Maths_Matrices.Tests;

public static class MatrixRowReductionAlgorithm<T> where T : INumber<T>
{
    public static (Matrix<T> m1, Matrix<T> m2) Apply(Matrix<T> m1, Matrix<T> m2)
    {
        Matrix<T> m = Matrix<T>.GenerateAugmentedMatrix(m1, m2);

        int i = 0;
        int j = 0;

        while (i < m.NbLines && j < m.NbColumns)
        {
            int maxRow = i;
            for (int w = i + 1; w < m.NbLines; w++)
            {
                if (T.Abs(m[w, j]) > T.Abs(m[maxRow, j]))
                {
                    maxRow = w;
                }
            }
            
            if (T.Abs(m[maxRow, j]) == T.Zero)
            {
                j++;
                continue;
            }
            if (maxRow != i)
            {
                MatrixElementaryOperations<T>.SwapLines(m,maxRow,i);
            }

            T pivot = m[i, j];
            for (int w = j; w < m.NbColumns; w++)
            {
                m[i, w] /= pivot;
            }

            for (int r = 0; r < m.NbLines; r++)
            {
                if (r != i)
                {
                    T factor = m[r, j];
                    for (int l = j; l < m.NbColumns; l++)
                    {
                        m[r, l] -= factor * m[i, l];
                    }
                }
            }

            i++;
            j++;
        }

        return m.Split(m1.NbColumns - 1);
    }
}
