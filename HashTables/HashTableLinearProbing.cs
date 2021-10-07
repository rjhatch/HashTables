using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    public class HashTableLinearProbing<K, D> : HashTable<K, D>
    {
        HashTableBucket[] table;
        int elementCount = 0;

        public HashTableLinearProbing(int numberOfElements)
        {
            tableSize = numberOfElements * 2;
            table = new HashTableBucket[tableSize];
        }

        /// <summary>
        /// Insert new value. Table can only hold the number of elements the table was initialied for.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns>true if the element was added, false otherwise.</returns>
        public override bool Insert(K key, D data)
        {
            //check if another element can be added.
            if (elementCount < tableSize / 2)
            {
                //search for an empty spot
                for (int i = 0; i < tableSize; i++)
                {
                    if (BucketEmpty(GetIndexWithModifier(key, i)))
                    {
                        table[GetIndexWithModifier(key, i)] = new HashTableBucket(key, data);
                        elementCount++;
                        return true;
                    }
                }
            }

            //too many elements.
            return false;
        }

        public override (bool success, K key, D data) Remove(K key)
        {
            //search for the value.
            int index = IndexSearch(key);

            if (index != -1)
            {
                var bucket = table[index];
                bucket.Deleted = true;
                elementCount--;
                return (true, bucket.Key, bucket.Data);
            }

            return (false, default, default);
        }

        /// <summary>
        /// Retrieve Value based on key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>(true, key, data) if found, (false, default, default) otherwise.</returns>
        public override (bool success, K key, D data) Retrieve(K key)
        {
            //search for the value.
            int index = IndexSearch(key);

            if (index != -1)
            {
                //return the values in the bucket.
                var bucket = table[index];
                return (true, bucket.Key, bucket.Data);
            }

            //the value was not found.
            return (false, default, default);
        }

        private bool BucketEmpty(int index)
        {
            //if the table index is null, return true.
            //else return deleted status.
            return table[index] == null || table[index].Deleted;
        }

        /// <summary>
        /// Searches for the index of the givven key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The index of the found bucket if found, -1 otherwise.</returns>
        private int IndexSearch(K key)
        {
            //search for the value.
            for (int i = 0; i < tableSize; i++)
            {
                int index = GetIndexWithModifier(key, i);

                //check if the bucket is not empty and the key matches.
                if (!BucketEmpty(index) && table[index].Key.Equals(key))
                    return index;
            }
            
            return -1;
        }
    }
}
