// Plato.NET
// Copyright (c) 2016 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.Interfaces;
using Plato.Messaging.Implementations.RMQ.Settings;
using System;
using Plato.Messaging.Implementations.RMQ.Interfaces;

namespace Plato.Messaging.Implementations.RMQ
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Messaging.Implementations.RMQ.RMQSenderBase" />
    /// <seealso cref="Plato.Messaging.Implementations.RMQ.Interfaces.IRMQSender" />
    public class RMQSender : RMQSenderBase, IRMQSender
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RMQSender"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public RMQSender(RMQSettings settings) : base(settings)
        {
        }

        /// <summary>
        /// Sends the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="action">The action.</param>
        public void Send(byte[] data, Action<ISenderProperties> action = null)
        {
            _Send(data, action);
        }
    }
}
