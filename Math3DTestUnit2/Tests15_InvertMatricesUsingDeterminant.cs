﻿using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests15_InvertMatricesUsingDeterminant
    {
        [Test]
        [DefaultFloatingPointTolerance(0.001d)]
        public void TestInvertMatrixInstance()
        {
            //If you need help, See => https://www.sangakoo.com/en/unit/inverse-matrix-using-determinants
            //Or you can reuse the same principle from course => https://www.wikihow.com/Find-the-Inverse-of-a-3x3-Matrix
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 2f, 3f, 8f },
                { 6f, 0f, -3f },
                { -1f, 3f, 2f },
            });

            Matrix<float> mInverted = m.InvertByDeterminant();

            Assert.That(mInverted.ToArray2D(), Is.EqualTo(new[,]
            {
                { 0.066f, 0.133f, -0.066f },
                { -0.066f, 0.088f, 0.4f },
                { 0.133f, -0.066f, -0.133f }
            }));
        }
        
        [Test]
        [DefaultFloatingPointTolerance(0.001d)]
        public void TestInvertMatrixStatic()
        {
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 1f, 2f },
                { 3f, 4f },
            });

            Matrix<float> mInverted = Matrix<float>.InvertByDeterminant(m);
            Assert.That(mInverted.ToArray2D(), Is.EqualTo(new[,]
            {
                { -2f, 1f },
                { 1.5f, -0.5f },
            }));
        }
        
        [Test]
        public void TestInvertImpossibleMatrix()
        {
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f },
            });

            Assert.Throws<MatrixInvertException<float>>(() =>
            {
                Matrix<float> mInverted = m.InvertByDeterminant();
            });
        }
        
    }
}