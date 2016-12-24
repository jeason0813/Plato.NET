﻿// Plato.NET
// Copyright (c) 2016 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;

namespace Plato.Threading.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ApplicationException" />
    [Serializable]
    public class WorkManagerException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkManagerException"/> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public WorkManagerException(string msg) : base(msg)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkManagerException"/> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="innerException">The inner exception.</param>
        public WorkManagerException(string msg, Exception innerException) : base(msg, innerException)
        {
        }
    }
}
