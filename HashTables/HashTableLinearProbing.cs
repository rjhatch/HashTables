using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class HashTableLinearProbing<K, D> : HashTable<K, D>
    {
        HashTableBucket[] table;

        public HashTableLinearProbing(int numberOfElements)
        {
            tableSize = numberOfElements * 2;
            table = new HashTableBucket[tableSize];
        }

        public override bool Insert(K key, D data)
        {
            throw new NotImplementedException();
        }

        public override (bool success, K key, D data) Remove(K key)
        {
            throw new NotImplementedException();
        }

        public override (bool success, K key, D data) Retrieve(K key)
        {
            throw new NotImplementedException();
        }
    }
}
