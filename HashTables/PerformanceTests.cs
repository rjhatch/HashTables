using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class PerformanceTests
    {
        int[] randomNumbers = new int[10000];
        Stopwatch timer = new Stopwatch();
        List<HTTimes> performanceData = new List<HTTimes>();
        StringBuilder sb = new StringBuilder();

        List<HashTableLinearProbing<int, int>> linearProbingList = new List<HashTableLinearProbing<int, int>>(3);
        List<HashTableChaining<int, int>> chaingList = new List<HashTableChaining<int, int>>(3);
        List<HashTableQuadraticProbing<int, int>> quadraticProbingList = new List<HashTableQuadraticProbing<int, int>>(3);

        private class HTTimes
        {
            public string name;
            public int count;
            public float InsertTicks, RetrieveTicks, DeleteTicks, InsertMs, RetrieveMs, DeleteMs;
        }

        public PerformanceTests()
        {
            Random rand = new Random();

            for (int i = 0; i < randomNumbers.Length; i++)
            {
                randomNumbers[i] = rand.Next(1000);
            }
        }

        public void RunTest(int count, string name, HashTable<int, int> tempTable)
        {
            if (count > 10000)
                throw new ArgumentOutOfRangeException();

            HTTimes tempTimes = new HTTimes();
            tempTimes.name = name;
            tempTimes.count = count;

            timer.Restart();

            for (int i = 0; i < count; i++)
            {
                tempTable.Insert(randomNumbers[i], randomNumbers[i] + 10);
            }

            timer.Stop();

            tempTimes.InsertTicks = timer.ElapsedTicks;
            tempTimes.InsertMs = timer.ElapsedMilliseconds;

            timer.Restart();

            for (int i = 0; i < count; i++)
            {
                tempTable.Retrieve(randomNumbers[i]);
            }

            timer.Stop();

            tempTimes.RetrieveTicks = timer.ElapsedTicks;
            tempTimes.RetrieveMs = timer.ElapsedMilliseconds;

            timer.Restart();

            for (int i = 0; i < count; i++)
            {
                tempTable.Remove(randomNumbers[i]);
            }

            timer.Stop();

            tempTimes.DeleteTicks = timer.ElapsedTicks;
            tempTimes.DeleteMs = timer.ElapsedMilliseconds;

            performanceData.Add(tempTimes);
        }

        public void DisplayPerformanceData()
        {
            List<string> countList = new List<string>();

            foreach (var item in performanceData)
            {
                if (!countList.Contains(item.count.ToString()))
                    countList.Add(item.count.ToString());
            }

            string tabs = new string('\t', countList.Count());

            sb.AppendLine("\t\t\tInsert" + tabs + "Ret." + tabs + "Remove");


            //Chaining
            AddLineData("Count", "Chaining", countList, countList, countList);

            List<string> insertList = performanceData.Where(p => p.name == "Chaining").OrderBy(p => p.count).Select(p => p.InsertTicks.ToString()).ToList();
            List<string> retrieveList = performanceData.Where(p => p.name == "Chaining").OrderBy(p => p.count).Select(p => p.RetrieveTicks.ToString()).ToList();
            List<string> deleteList = performanceData.Where(p => p.name == "Chaining").OrderBy(p => p.count).Select(p => p.DeleteTicks.ToString()).ToList();

            AddLineData("".PadRight(5), "Ticks".PadRight(8), insertList, retrieveList, deleteList);

            insertList = performanceData.Where(p => p.name == "Chaining").OrderBy(p => p.count).Select(p => p.InsertMs.ToString()).ToList();
            retrieveList = performanceData.Where(p => p.name == "Chaining").OrderBy(p => p.count).Select(p => p.RetrieveMs.ToString()).ToList();
            deleteList = performanceData.Where(p => p.name == "Chaining").OrderBy(p => p.count).Select(p => p.DeleteMs.ToString()).ToList();

            AddLineData("".PadRight(5), "Ms".PadRight(8), insertList, retrieveList, deleteList);

            sb.AppendLine();

            //LinearProbing
            AddLineData("Count", "LP".PadRight(8), countList, countList, countList);

            insertList = performanceData.Where(p => p.name == "LP").OrderBy(p => p.count).Select(p => p.InsertTicks.ToString()).ToList();
            retrieveList = performanceData.Where(p => p.name == "LP").OrderBy(p => p.count).Select(p => p.RetrieveTicks.ToString()).ToList();
            deleteList = performanceData.Where(p => p.name == "LP").OrderBy(p => p.count).Select(p => p.DeleteTicks.ToString()).ToList();

            AddLineData("".PadRight(5), "Ticks".PadRight(8), insertList, retrieveList, deleteList);

            insertList = performanceData.Where(p => p.name == "LP").OrderBy(p => p.count).Select(p => p.InsertMs.ToString()).ToList();
            retrieveList = performanceData.Where(p => p.name == "LP").OrderBy(p => p.count).Select(p => p.RetrieveMs.ToString()).ToList();
            deleteList = performanceData.Where(p => p.name == "LP").OrderBy(p => p.count).Select(p => p.DeleteMs.ToString()).ToList();

            AddLineData("".PadRight(5), "Ms".PadRight(8), insertList, retrieveList, deleteList);

            sb.AppendLine();

            //Quadratic Probing
            AddLineData("Count", "QP".PadRight(8), countList, countList, countList);

            insertList = performanceData.Where(p => p.name == "QP").OrderBy(p => p.count).Select(p => p.InsertTicks.ToString()).ToList();
            retrieveList = performanceData.Where(p => p.name == "QP").OrderBy(p => p.count).Select(p => p.RetrieveTicks.ToString()).ToList();
            deleteList = performanceData.Where(p => p.name == "QP").OrderBy(p => p.count).Select(p => p.DeleteTicks.ToString()).ToList();

            AddLineData("".PadRight(5), "Ticks".PadRight(8), insertList, retrieveList, deleteList);

            insertList = performanceData.Where(p => p.name == "QP").OrderBy(p => p.count).Select(p => p.InsertMs.ToString()).ToList();
            retrieveList = performanceData.Where(p => p.name == "QP").OrderBy(p => p.count).Select(p => p.RetrieveMs.ToString()).ToList();
            deleteList = performanceData.Where(p => p.name == "QP").OrderBy(p => p.count).Select(p => p.DeleteMs.ToString()).ToList();

            AddLineData("".PadRight(5), "Ms".PadRight(8), insertList, retrieveList, deleteList);

            Console.Write(sb.ToString());
            File.WriteAllText("Results.txt", sb.ToString());
        }

        private void AddLineData(string header, string name, List<string> insertList, List<string> retrieveList, List<string> deleteList)
        {
            int count = insertList.Count() + retrieveList.Count() + deleteList.Count();

            sb.Append($"{header}\t{name}\t");

            sb.AppendJoin('\t', insertList);
            sb.Append('\t');

            sb.AppendJoin('\t', retrieveList);
            sb.Append('\t');

            sb.AppendJoin('\t', deleteList);

            sb.AppendLine();
        }
    }
}
