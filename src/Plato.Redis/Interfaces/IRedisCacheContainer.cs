﻿using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Plato.Redis.Interfaces
{
    public interface IRedisCacheContainer
    {
        bool SupportsExpiration { get; }

        RedisValue Get(string key);
        Task<RedisValue> GetAsync(string key);
        bool Remove(string key);
        Task<bool> RemoveAsync(string key);
        bool Set(string key, RedisValue value, TimeSpan? keepAlive = default(TimeSpan?));
        Task<bool> SetAsync(string key, RedisValue value, TimeSpan? keepAlive = default(TimeSpan?));
    }
}