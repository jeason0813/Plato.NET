﻿// Plato.NET
// Copyright (c) 2017 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using MsgPack.Serialization;
using Plato.Redis.Interfaces;
using StackExchange.Redis;
using System.IO;

namespace Plato.Redis.Serializers
{
    /// <summary>
    /// 
    /// </summary>    
    /// <seealso cref="Plato.Redis.Interfaces.IRedisSerializer" />
    public class MsgPackRedisSerializer : IRedisSerializer
    {
        /// <summary>
        /// Serializes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public RedisValue Serialize(object data)
        {
            if (data != null)
            {
                using (var ms = new MemoryStream())
                {
                    MessagePackSerializer.Get(data.GetType()).Pack(ms, data);
                    return ms.ToArray();
                }
            }

            return RedisValue.EmptyString;
        }

        /// <summary>
        /// Deserializes the specified data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public T Deserialize<T>(RedisValue data)
        {
            if (data.HasValue)
            {
                using (var ms = new MemoryStream(data))
                {
                    return MessagePackSerializer.Get<T>().Unpack(ms);
                }
            }

            return default(T);
        }
    }
}