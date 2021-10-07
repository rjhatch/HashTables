using HashTables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTablesTests
{
    [TestClass]
    public class LinearProbingTests
    {
        [TestMethod]
        public void Insert()
        {
            HashTableLinearProbing<int, int> table = new HashTableLinearProbing<int, int>(10);

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(true, table.Insert(i, i + 10));
            }

            Assert.IsFalse(table.Insert(11, 21));
        }

        [TestMethod]
        public void FindValue()
        {
            HashTableLinearProbing<int, int> table = new HashTableLinearProbing<int, int>(10);

            for (int i = 0; i < 10; i++)
            {
                table.Insert(i, i + 10);
            }

            Assert.AreEqual((true, 3, 13), table.Retrieve(3));
        }

        [TestMethod]
        public void FindValueWithCollisions()
        {
            HashTableLinearProbing<int, int> table = new HashTableLinearProbing<int, int>(10);

            for (int i = 0; i < 5; i++)
            {
                table.Insert(i, i + 10);
                table.Insert(i, i + 10);
            }

            Assert.AreEqual((true, 3, 13), table.Retrieve(3));
        }

        [TestMethod]
        public void FindNonPresentValueWithCollisions()
        {
            HashTableLinearProbing<int, int> table = new HashTableLinearProbing<int, int>(10);

            for (int i = 0; i < 5; i++)
            {
                table.Insert(i, i + 10);
                table.Insert(i, i + 10);
            }

            Assert.AreEqual((false, default, default), table.Retrieve(20));
        }

        [TestMethod]
        public void RemoveValue()
        {
            HashTableLinearProbing<int, int> table = new HashTableLinearProbing<int, int>(10);

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(true, table.Insert(i, i + 10));
            }

            Assert.AreEqual((true, 3, 13), table.Remove(3));
            Assert.AreEqual((false, default, default), table.Retrieve(3));
        }

        [TestMethod]
        public void RemoveValueWithCollisions()
        {
            HashTableLinearProbing<int, int> table = new HashTableLinearProbing<int, int>(10);

            for (int i = 0; i < 5; i++)
            {
                table.Insert(i, i + 10);
                table.Insert(i, i + 10);
            }

            Assert.AreEqual((true, 3, 13), table.Remove(3));
            Assert.AreEqual((true, 3, 13), table.Retrieve(3));
            Assert.AreEqual((true, 3, 13), table.Remove(3));
            Assert.AreEqual((false, default, default), table.Retrieve(3));
        }
    }
}
