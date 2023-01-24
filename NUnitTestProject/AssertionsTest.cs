namespace NUnitTestProject
{
    internal class AssertionsTest
    {
        [Test]
        public void FailTest()
        {
            Assert.Fail();
        }

        [Test]
        public void EmptyTest()
        {
            string myString = "";
            Assert.IsEmpty(myString);
        }

        [Test]
        public void CollectionTest()
        {
            List<int> intsList = new List<int>()
            {
                1, 2,3, 4, 0,
            };
            Assert.Contains(3, intsList);
            Assert.True(intsList.TrueForAll(x => x >= 0));
            Assert.True(intsList.Count != 0);
        }

        [Test]
        public void ExceptionTest()
        {
            Assert.Throws<NullReferenceException>(NullMethod);
        }

        private void NullMethod()
        {
            throw new NullReferenceException();
        }

        [Test]
        public void CollectionTest2()
        {
            List<string> strings1 = new List<string>() { "bruh", "kek", "amogus", "kekas" };
            List<string> strings2 = new List<string>() { "amogus", "bruh", "kek", "kekas" };
            bool IsEqual = false;
            foreach (var s1 in strings1)
            {
                if (strings2.Contains(s1) && strings1.Count == strings2.Count)
                {
                    IsEqual= true;
                }
                else
                {
                    IsEqual= false;
                }
            }
            Assert.True(IsEqual);
        }

        [Test]
        public void TrueFalseTest()
        {
            int myInt=9;
            Assert.True(myInt is int);
        }

        [Test]
        public void StringsTest()
        {
            string s1 = "bruh";
            string s2 = "bruh";
            Assert.AreEqual(s2,s1);
        }
    }
}
