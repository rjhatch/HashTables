using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    public class HashTableChaining<K, D> : HashTable<K, D>
    {
        List<HashTableBucket>[] table;

        public HashTableChaining(int numberOfElements)
        {
            tableSize = numberOfElements;
            table = new List<HashTableBucket>[tableSize];
        }

        public override bool Insert(K key, D data)
        {
            int index = GetIndex(key);

            //check to see if the list is null, instatiate it.
            if (table[index] == null)
                table[index] = new List<HashTableBucket>();

            //add the bucket.
            table[GetIndex(key)].Add(new HashTableBucket(key, data));

            return true;
        }

        public override (bool success, K key, D data) Remove(K key)
        {
            //find the index.
            int tableIndex = GetIndex(key);

            //see if the value is there.
            int index = table[tableIndex].FindIndex(b => b.Key.Equals(key));

            if (index == -1)
                return (false, default, default);
            else
            {
                var bucket = table[tableIndex].ElementAt(index);
                table[tableIndex].RemoveAt(index);
                return (true, bucket.Key, bucket.Data);
            }
        }

        public override (bool success, K key, D data) Retrieve(K key)
        {
            var bucket = GetBucket(key);
            return bucket != null ? (true, bucket.Key, bucket.Data) : (false, default, default);
        }

        /// <summary>
        /// Get the bucket based on the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The bucket if found, null otherwise.</returns>
        private HashTableBucket GetBucket(K key)
        {
            int tableIndex = GetIndex(key);
            int index = table[tableIndex].FindIndex(b => b.Key.Equals(key));

            //if the bucket is there, return it.
            if (index != -1)
                return table[tableIndex].ElementAt(index);

            //if not, return null.
            return null;
        }
    }
}
