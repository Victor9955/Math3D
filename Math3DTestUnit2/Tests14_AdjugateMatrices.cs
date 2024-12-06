﻿using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests14_AdjugateMatrices
    {
        [Test]
        [DefaultFloatingPointTolerance(0.001d)]
        public void TestCalculateAdjugateMatrixInstance()
        {
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            });

            Matrix<float> adjM = m.Adjugate();
            Assert.That(adjM.ToArray2D(), Is.EqualTo(new[,]
            {
                { 4f, -2f },
                { -3f, 1f },
            }));
        }

        [Test]
        [DefaultFloatingPointTolerance(0.001d)]
        public void TestCalculateAdjugateMatrixStatic()
        {
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 1f, 0f, 5f },
                { 2f, 1f, 6f },
                { 3f, 4f, 0f },
            });

            Matrix<float> adjM = Matrix<float>.Adjugate(m);
            Assert.That(adjM.ToArray2D(), Is.EqualTo(new[,]
            {
                { -24f, 20f, -5f },
                { 18f, -15f, 4f },
                { 5f, -4f, 1f },
            }));
        }

        [Test]
        [DefaultFloatingPointTolerance(0.001d)]
        public void TestCalculateAdjugateMatrixIdentity4x4()
        {
            Matrix<float> m = new Matrix<float>(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            });

            Matrix<float> adjM = m.Adjugate();
            Assert.That(adjM.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }));
        }
    }
}