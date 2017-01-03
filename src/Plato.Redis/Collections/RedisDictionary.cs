﻿// Plato.NET
// Copyright (c) 2016 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Newtonsoft.Json;
using Plato.Redis.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Plato.Redis.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="Plato.Redis.RedisControl" />
    /// <seealso cref="Plato.Redis.Interfaces.IRedisDictionary{TKey, TValue}" />
    public class RedisDictionary<TKey, TValue> : RedisControl, IRedisDictionary<TKey, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="redisDb">The redis database.</param>
        /// <param name="redisKey">The redis key.</param>
        public RedisDictionary(IDatabase redisDb, string redisKey) : base(redisDb, redisKey)
        {
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        protected virtual string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// De-serializes the specified serialized.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialized">The serialized.</param>
        /// <returns></returns>
        protected virtual T Deserialize<T>(string serialized)
        {
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        public void Add(TKey key, TValue value)
        {
            RedisDb.HashSet(RedisKey, Serialize(key), Serialize(value));
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="when">The when.</param>
        /// <returns></returns>
        public bool Add(TKey key, TValue value, When when)
        {
            return RedisDb.HashSet(RedisKey, Serialize(key), Serialize(value), when: when);
        }
        
        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.
        /// </returns>
        public bool ContainsKey(TKey key)
        {
            return RedisDb.HashExists(RedisKey, Serialize(key));
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </returns>
        public bool Remove(TKey key)
        {
            return RedisDb.HashDelete(RedisKey, Serialize(key));
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.
        /// </returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            var redisValue = RedisDb.HashGet(RedisKey, Serialize(key));
            if (redisValue.IsNull)
            {
                value = default(TValue);
                return false;
            }

            value = Deserialize<TValue>(redisValue.ToString());
            return true;

        }

        /// <summary>
        /// Gets the or add.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="addFunction">The add function.</param>
        /// <returns></returns>
        public TValue GetOrAdd(TKey key, Func<TKey,TValue> addFunction)
        {
            TValue value;
            if(!TryGetValue(key, out value))
            {
                value = addFunction(key);
                Add(key, value);
            }

            return value;
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return new Collection<TValue>(RedisDb.HashValues(RedisKey).Select(h => Deserialize<TValue>(h.ToString())).ToList()); }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get { return new Collection<TKey>(RedisDb.HashKeys(RedisKey).Select(h => Deserialize<TKey>(h.ToString())).ToList()); }
        }

        /// <summary>
        /// Gets or sets the <see cref="TValue"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="TValue"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                var redisValue = RedisDb.HashGet(RedisKey, Serialize(key));
                return redisValue.IsNull ? default(TValue) : Deserialize<TValue>(redisValue.ToString());
            }
            set
            {
                Add(key, value);
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public void Clear()
        {
            RedisDb.KeyDelete(RedisKey);
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return RedisDb.HashExists(RedisKey, Serialize(item.Key));
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            var values = RedisDb.HashGetAll(RedisKey);
            for (var i = 0; i < values.Length; i++)
            {
                var key = Deserialize<TKey>(values[i].Name.ToString());
                var value = Deserialize<TValue>(values[i].Value.ToString());

                array[i + arrayIndex] = new KeyValuePair<TKey, TValue>(key, value);
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public int Count
        {
            get { return (int)RedisDb.HashLength(RedisKey); }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var hashKey in RedisDb.HashKeys(RedisKey))
            {
                var redisValue = RedisDb.HashGet(RedisKey, hashKey);
                yield return new KeyValuePair<TKey, TValue>(Deserialize<TKey>(hashKey.ToString()), Deserialize<TValue>(redisValue.ToString()));
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return GetEnumerator();
        }

        /// <summary>
        /// Adds the multiple.
        /// </summary>
        /// <param name="items">The items.</param>
        public void AddMultiple(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            RedisDb.HashSet(RedisKey, items.Select(i => new HashEntry(Serialize(i.Key), Serialize(i.Value))).ToArray());
        }
    }
}