﻿// Plato.NET
// Copyright (c) 2017 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;

namespace Plato.Cache
{
    /// <summary>
    ///
    /// </summary>
    public class CacheDataInfo<T>
    {        
        /// <summary>
        /// Gets or sets the new cache data.
        /// </summary>
        /// <value>
        /// The new cache data.
        /// </value>
        public T NewCacheData { get; set; }

        /// <summary>
        /// Gets or sets the keep alive.
        /// </summary>
        /// <value>
        /// The keep alive.
        /// </value>
        public TimeSpan KeepAlive { get; set; }
    }
}
