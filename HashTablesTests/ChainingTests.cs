using HashTables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTablesTests
{
    [TestClass]
    public class ChainingTests
    {
        [TestMethod]
        public void InsertAndRetrieve()
        {
            HashTableChaining<int, int> table = new HashTableChaining<int, int>(10);

            for (int i = 0; i < 10; i++)
            {
                table.Insert(i, i + 10);
            }

            Assert.AreEqual((true, 1, 11), table.Retrieve(1));
        }

        [TestMethod]
        public void Remove()
        {
            HashTableChaining<int, int> table = new HashTableChaining<int, int>(10);

            for (int i = 0; i < 10; i++)
            {
                table.Insert(i, i + 10);
            }
            for (int i = 0; i < 10; i++)
            {
                table.Insert(i, i + 10);
            }

            Assert.AreEqual((true, 1, 11), table.Remove(1));
            Assert.AreEqual((true, 1, 11), table.Remove(1));
            Assert.AreEqual((false, default, default), table.Remove(1));
        }
    }
}