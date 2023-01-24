using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace NUnitTestProject
{
    [TestFixture, Parallelizable]
    [Ignore("kek")]
    public class CalculatorTest
    {
        [Test, Order(3)]
        [TestCase(1, 2)]
        public void ConstructorTest(int x, int y)
        {
            Calculator myCalculator = new Calculator(x, y);
            Assert.That(1, Is.EqualTo(myCalculator.x));
            Assert.That(2, Is.EqualTo(myCalculator.y));
        }

        [Test]
        //[Test, Order(1)]
        [TestCaseSource(typeof(PlusMethodSource))]
        public void PlusMethodTest(int num1, int num2, int expectedResult)
        {
            Assert.That(num1 + num2, Is.EqualTo(expectedResult));
        }

        [Test]
        //[Test, Order(2)]
        [TestCaseSource(typeof(MinusMethodSource))]
        public void MinusMethodTest(int num1, int num2, int expectedResult)
        {
            Assert.That(num1 - num2, Is.EqualTo(expectedResult));
        }

        //[Test]
        //public void ParallelTestMethodCall()
        //{
        //    Parallel.Invoke(() => MinusMethodTest(1, 2, -1), () => PlusMethodTest(1, 2, 3));
        //}

        public class PlusMethodSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new int[] { 3, 5, 8 };
                yield return new int[] { -9, 0, -9 };
                yield return new int[] { -18, 18, 0 };
            }
        }

        public class MinusMethodSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new int[] { 40, 5, 35 };
                yield return new int[] { -9, 3, -12 };
                yield return new int[] { -18, 18, -36 };
            }
        }
    }
}