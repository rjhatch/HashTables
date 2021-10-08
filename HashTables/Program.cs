using System;

namespace HashTables
{
    class Program
    {
        static void Main(string[] args)
        {
            PerformanceTests performanceTests = new PerformanceTests();

            performanceTests.RunTest(100, "Chaining", new HashTableChaining<int, int>(100));
            performanceTests.RunTest(1000, "Chaining", new HashTableChaining<int, int>(1000));
            performanceTests.RunTest(10000, "Chaining", new HashTableChaining<int, int>(10000));

            performanceTests.RunTest(100, "LP", new HashTableLinearProbing<int, int>(100));
            performanceTests.RunTest(1000, "LP", new HashTableLinearProbing<int, int>(1000));
            performanceTests.RunTest(10000, "LP", new HashTableLinearProbing<int, int>(10000));

            performanceTests.RunTest(100, "QP", new HashTableQuadraticProbing<int, int>(100));
            performanceTests.RunTest(1000, "QP", new HashTableQuadraticProbing<int, int>(1000));
            performanceTests.RunTest(10000, "QP", new HashTableQuadraticProbing<int, int>(10000));

            performanceTests.DisplayPerformanceData();
        }
    }
}
