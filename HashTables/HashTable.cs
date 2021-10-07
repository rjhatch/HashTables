using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    public abstract class HashTable<K, D>
    {
        protected int tableSize;

        protected class HashTableBucket
        {
            public K Key { get; private set; }
            public D Data { get; private set; }
            public bool Deleted { get; set; } = false;

            public HashTableBucket(K key, D data)
            {
                Key = key;
                Data = data;
            }
        }

        /// <summary>
        /// Insert Key/Value pair.
        /// </summary>
        /// <param name="key">Generic type identifier.</param>
        /// <param name="data">Generic type data.</param>
        /// <returns>True if successful, false if not.</returns>
        public abstract bool Insert(K key, D data);

        /// <summary>
        /// Retrieve a value based on key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>(true, key, data) if found, (false, default, default) otherwise.</returns>
        public abstract (bool success, K key, D data) Retrieve(K key);

        /// <summary>
        /// Remove a value based on key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>(true, key, data) if successfull, (false, default, default) otherwise.</returns>
        public abstract (bool success, K key, D data) Remove(K key);
        
        /// <summary>
        /// Index of the table based on key hashcode.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Index on the table.</returns>
        protected int GetIndex(K key)
        {
            return key.GetHashCode() % tableSize;
        }

        /// <summary>
        /// Index of the table based on key hashcode plus modifier.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Index on the table.</returns>
        protected int GetIndexWithModifier(K key, int modifier)
        {
            return (key.GetHashCode() + modifier) % tableSize;
        }
    }
}
