using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests04_ScalarMultiplication
    {
        [Test]
        public void TestScalarMultiplicationInstance()
        {
            Matrix<int> m = new Matrix<int>(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });

            m.Multiply(2);

            Assert.That(m.ToArray2D(), Is.EqualTo(new[,]
            {
                { 2, 4, 6 },
                { 8, 10, 12 },
                { 14, 16, 18 },
            }));
        }

        [Test]
        public void TestScalarMultiplicationStatic()
        {
            Matrix<int> m = new Matrix<int>(new[,]
            {
                { 0, 0, 0 },
                { 0, 5, 0 },
                { 0, 0, 0 }
            });

            Matrix<int> m2 = Matrix<int>.Multiply(m, 5);

            Assert.That(m2.ToArray2D(), Is.EqualTo(new[,]
            {
                { 0, 0, 0 },
                { 0, 25, 0 },
                { 0, 0, 0 },
            }));

            Assert.That(m.ToArray2D(), Is.EqualTo(new[,]
            {
                { 0, 0, 0 },
                { 0, 5, 0 },
                { 0, 0, 0 },
            }));
        }

        [Test]
        public void TestScalarMultiplicationOperator()
        {
            Matrix<int> m = new Matrix<int>(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });

            //See Operator overloading documentation =>
            //https://docs.microsoft.com/fr-fr/dotnet/csharp/language-reference/operators/operator-overloading
            Matrix<int> m2 = m * 2;

            Assert.That(m2.ToArray2D(), Is.EqualTo(new[,]
            {
                { 2, 4, 6 },
                { 8, 10, 12 },
                { 14, 16, 18 },
            }));

            Assert.That(m.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            }));

            Matrix<int> m4 = 4 * m;
            
            Assert.That(m4.ToArray2D(), Is.EqualTo(new[,]
            {
                { 4, 8, 12 },
                { 16, 20, 24 },
                { 28, 32, 36 },
            }));
        }
        
        
        [Test]
        public void TestNegativeMatrix()
        {
            Matrix<int> m1 = new Matrix<int> (new int[,]
            {
                { -1, 2, 3 },
                { 4, -5, 6 },
                { -7, 8, 9 }
            });

            Matrix<int> m2 = -m1;
            
            Assert.That(m2.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1, -2, -3 },
                { -4, 5, -6 },
                { 7, -8, -9 }
            }));
        }
        
    }
}