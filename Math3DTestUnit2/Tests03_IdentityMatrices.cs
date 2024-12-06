using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests03IdentityMatrices
    {
        [Test]
        public void TestGenerateIdentityMatrices()
        {
            Matrix<int> identity2 = Matrix<int>.Identity(2);
            Assert.That(identity2.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1, 0 },
                { 0, 1 },
            }));

            Matrix<int> identity3 = Matrix<int>.Identity(3);
            Assert.That(identity3.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 },
            }));

            Matrix<int> identity4 = Matrix<int>.Identity(4);
            Assert.That(identity4.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 },
            }));
        }

        [Test]
        public void TestMatricesIsIdentity()
        {
            Matrix<int> identity2 = new Matrix<int>(new[,]
            {
                { 1, 0 },
                { 0, 1 }
            });
            Assert.IsTrue(identity2.IsIdentity());

            Matrix<int> identity3 = new Matrix<int>(new[,]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 }
            });
            Assert.IsTrue(identity3.IsIdentity());

            Matrix<int> notSameColumnsAndLines = new Matrix<int>(new[,]
            {
                { 1, 0, 0 },
                { 0, 1, 0 }
            });
            Assert.IsFalse(notSameColumnsAndLines.IsIdentity());

            Matrix<int> notIdentity1 = new Matrix<int>(new[,]
            {
                { 1, 0, 0 },
                { 0, 2, 0 },
                { 0, 0, 3 },
            });
            Assert.IsFalse(notIdentity1.IsIdentity());

            Matrix<int> notIdentity2 = new Matrix<int>(new[,]
            {
                { 1, 0, 4 },
                { 0, 1, 0 },
                { 0, 0, 1 },
            });
            Assert.IsFalse(notIdentity2.IsIdentity());
        }
    }
}