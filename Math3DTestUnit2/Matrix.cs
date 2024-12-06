using System;
using System.Numerics;
using Maths_Matrices.Tests;
using NUnit.Framework.Constraints;
using InvalidOperationException = System.InvalidOperationException;

public class Matrix<T> where T : INumber<T>
{
    private T[,] matrix;
    public Matrix(int lines, int columns)
    {
        matrix = new T[lines, columns];
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                matrix[i, j] = T.Zero;
            }
        }
    }
    public Matrix(T[,] array) { matrix = array; }
    public Matrix(Matrix<T> m_matrix)
    {
        matrix = new T[m_matrix.NbLines, m_matrix.NbColumns];
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                matrix[i, j] = m_matrix[i, j];
            }
        }
    }
    
    public T this[int x, int y] { get { return matrix[x, y]; } set { matrix[x, y] = value; } }
    
    public int NbLines { get => matrix.GetLength(0);}
    public int NbColumns { get => matrix.GetLength(1);}

    public T[,] ToArray2D() { return matrix; }

    public static Matrix<T> Identity(int size)
    {
        Matrix<T> cashMatrix = new Matrix<T>(new T[size, size]);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cashMatrix[i,j] = i == j ? T.One : T.Zero;
            }
        }
        return cashMatrix;
    }

    public bool IsIdentity()
    {
        if(NbLines != NbColumns) return false;
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                if ((i == j && matrix[i, j] != T.One) || (i != j && matrix[i, j] != T.Zero))
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    public static Matrix<T> Multiply(Matrix<T> m_matrix,T a)
    {
        Matrix<T> cashMatrix = new Matrix<T>(m_matrix);
        for (int i = 0; i < cashMatrix.NbLines; i++)
        {
            for (int j = 0; j < cashMatrix.NbColumns; j++)
            {
                cashMatrix[i,j] =  a * m_matrix[i,j];
            }
        }
        return cashMatrix;
    }
    
    public static Matrix<T> Multiply(Matrix<T> m,Matrix<T> m2)
    {   if(m.NbColumns != m2.NbLines) throw new MatrixMultiplyException<T>();
        Matrix<T> cashMatrix = new Matrix<T>(m.NbLines, m2.NbColumns);
        for (int i = 0; i < cashMatrix.NbLines; i++)
        {
            for (int j = 0; j < cashMatrix.NbColumns; j++)
            {
                for (int k = 0; k < m.NbColumns; k++)
                {
                    cashMatrix[i,j] += m[i,k] * m2[k,j];
                }
            }
        }
        return cashMatrix;
    }
    
    public void Multiply(T a) { matrix = Multiply(this, a).ToArray2D(); }
    
    public Matrix<T> Multiply(Matrix<T> m_matrix) { return Multiply(this,m_matrix); }
   
    public static Matrix<T> operator *(Matrix<T> m,T a) { return Multiply(m,a); }
    
    public static Matrix<T> operator *(T a,Matrix<T> m) { return Multiply(m,a); }
    
    public static Matrix<T> operator *(Matrix<T> m,Matrix<T> m2) { return Multiply(m,m2); }
    
   public static Matrix<T> operator -(Matrix<T> m) { return Multiply(m,-T.One); }
   
   public static Matrix<T> operator -(Matrix<T> m,Matrix<T> m2)
   {
       Matrix<T> cashMatrix = new Matrix<T>(m.NbLines, m.NbColumns);
       for (int i = 0; i < cashMatrix.NbLines; i++)
       {
           for (int j = 0; j < cashMatrix.NbColumns; j++)
           {
               cashMatrix[i,j] =  m[i,j] - m2[i,j];
           }
       }
       return cashMatrix;
   }

   public void Add(Matrix<T> m)
   {
       if(m.NbLines != NbLines || m.NbColumns != NbColumns) throw new MatrixSumException<T>();
       for (int i = 0; i < NbLines; i++)
       {
           for (int j = 0; j < NbColumns; j++)
           {
               matrix[i,j] += m[i,j];
           }
       }
   }
   
   public static Matrix<T> Add(Matrix<T> m,Matrix<T> m2)
   {
       Matrix<T> cashMatrix = new Matrix<T>(m);
       cashMatrix.Add(m2);
       return cashMatrix;
   }
   
   public static Matrix<T> operator +(Matrix<T> m,Matrix<T> m2) { return Add(m,m2);}

   public Matrix<T> Transpose() { return Transpose(this); }
   
   public static Matrix<T> Transpose(Matrix<T> m)
   {
       Matrix<T> cashMatrix = new Matrix<T>(m.NbColumns, m.NbLines);
       for (int i = 0; i < cashMatrix.NbLines; i++)
       {
           for (int j = 0; j < cashMatrix.NbColumns; j++)
           {
               cashMatrix[i,j] = m.matrix[j,i];
           }
       }
       return cashMatrix;
   }

   public static Matrix<T> GenerateAugmentedMatrix(Matrix<T> m1, Matrix<T> m2)
   {
       Matrix<T> cashMatrix = new Matrix<T>(m1.NbLines, m1.NbColumns + m2.NbColumns);
    
       for (int i = 0; i < cashMatrix.NbLines; i++)
       {
           for (int j = 0; j < cashMatrix.NbColumns; j++)
           {
               cashMatrix[i, j] = (j < m1.NbColumns) ? m1[i, j] : m2[i, j - m1.NbColumns];
           }
       }
    
       return cashMatrix;
   }


   public (Matrix<T> m1, Matrix<T> m2) Split(int p)
   {
       Matrix<T> result1 = new Matrix<T>(NbLines, p + 1);
       Matrix<T> result2 = new Matrix<T>(NbLines, NbColumns - (p + 1));

       for (int i = 0; i < NbLines; i++)
       {
           for (int j = 0; j < NbColumns; j++)
           {
               if (j < p + 1)
               {
                   result1[i, j] = matrix[i, j];
               }
               else
               {
                   result2[i, j - (p + 1)] = matrix[i, j];
               }
           }
       }

       return (result1, result2);
   }


   public static Matrix<T> InvertByRowReduction(Matrix<T> m)
   {
       if (Determinant(m) == T.Zero) throw new MatrixInvertException<T>();
       return MatrixRowReductionAlgorithm<T>.Apply(m, Identity(m.NbColumns)).m2;
   }
   
   public Matrix<T> InvertByRowReduction() { return InvertByRowReduction(this); }

   public static Matrix<T> InvertByDeterminant(Matrix<T> m)
   {
       if(Determinant(m) == T.Zero) throw new MatrixInvertException<T>();
       return T.One / Determinant(m) * m.Adjugate();
   }
   
   public Matrix<T> InvertByDeterminant() { return InvertByDeterminant(this); }

   public Matrix<T> SubMatrix(int p0, int p1) { return SubMatrix(this, p0, p1); }
   
   public static Matrix<T> SubMatrix(Matrix<T> m, int c, int l)
   {
       Matrix<T> result = new Matrix<T>(m.NbLines - 1, m.NbColumns - 1);
       for (int i = 0; i < result.NbLines; i++)
       {
           for (int j = 0; j < result.NbColumns; j++)
           {
               int skipI = i;
               if (skipI >= c)
               {
                   skipI = i + 1;
               }
               int skipJ = j;
               if (skipJ >= l)
               {
                   skipJ = j + 1;
               }
               result[i, j] = m[skipI, skipJ];
           }
       }
       return result;
   }

   public static T Determinant(Matrix<T> m)
   {
       if (m.NbLines != m.NbColumns) throw new InvalidOperationException();
       
       if (m.NbLines == 2 && m.NbColumns == 2)
       {
           return m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
       }

       T det = T.Zero;
       for (int col = 0; col < m.NbColumns; col++)
       {
           Matrix<T> subMatrix = SubMatrix(m, 0, col);
           det = col % 2 == 0 ? det + m[0, col] * Determinant(subMatrix) : det - m[0, col] * Determinant(subMatrix);
       }
       return det;
   }

   public static Matrix<T> Adjugate(Matrix<T> m)
   {
       if (m.NbLines != m.NbColumns) throw new InvalidOperationException();
       Matrix<T> adjugateMatrix = new Matrix<T>(m.NbLines, m.NbColumns);
       
       if (m.NbLines == 2 && m.NbColumns == 2)
       {
           adjugateMatrix[0, 0] = m[1, 1];
           adjugateMatrix[0, 1] = -m[0, 1];
           adjugateMatrix[1, 0] = -m[1, 0];
           adjugateMatrix[1, 1] = m[0, 0];
           return adjugateMatrix;
       }

       for (int i = 0; i < m.NbLines; i++)
       {
           for (int j = 0; j < m.NbColumns; j++)
           {
               Matrix<T> subMatrix = SubMatrix(m, i, j);
               T cofactor = (i + j) % 2 == 0 ? T.One : -T.One;
               cofactor *= Determinant(subMatrix);
               adjugateMatrix[i, j] = cofactor;
           }
       }

       return adjugateMatrix.Transpose();
   }
   public Matrix<T> Adjugate() { return Adjugate(this); }
}