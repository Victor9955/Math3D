﻿using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests02CopyAndModifyMatrices
    {
        [Test]
        public void TestModifyMatrix()
        {
            Matrix<int> m = new Matrix<int>(2, 2);
            Assert.That(m.ToArray2D(), Is.EqualTo(new[,]
            {
                { 0, 0 },
                { 0, 0 },
            }));

            //See Indexers Documentation =>
            //https://docs.microsoft.com/fr-fr/dotnet/csharp/programming-guide/indexers/
            m[0, 0] = 1;
            m[0, 1] = 2;
            m[1, 0] = 3;
            m[1, 1] = 4;

            Assert.That(m.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1, 2 },
                { 3, 4 },
            }));
        }

        [Test]
        public void TestCopyAndChangeMatrices()
        {
            Matrix<int> m1 = new Matrix<int>(new[,]
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 7, 8, 9 },
                }
            );

            Matrix<int> m2 = new Matrix<int>(m1);
            m2[0, 0] = 23;

            Assert.That(m2.ToArray2D(), Is.EqualTo(new[,]
            {
                { 23, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
            }));

            Assert.That(m1.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
            }));
        }
    }
}