// Plato.NET
// Copyright (c) 2017 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.Implementations.RMQ.Interfaces;
using Plato.Messaging.Implementations.RMQ.Settings;
using System.Threading;

namespace Plato.Messaging.Implementations.RMQ
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Messaging.Implementations.RMQ.RMQConsumer" />
    /// <seealso cref="Plato.Messaging.Implementations.RMQ.Interfaces.IRMQConsumerByte" />
    public class RMQConsumerByte : RMQConsumer, IRMQConsumerByte
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RMQConsumerByte" /> class.
        /// </summary>
        /// <param name="connectionFactory">The connection factory.</param>
        /// <param name="connectionSettings">The connection settings.</param>
        /// <param name="queueSettings">The settings.</param>
        public RMQConsumerByte(
            IRMQConnectionFactory connectionFactory,
            RMQConnectionSettings connectionSettings,
            RMQQueueSettings queueSettings) 
            : base(connectionFactory, connectionSettings, queueSettings)
        {
        }

        /// <summary>
        /// Receives the specified msec timeout.
        /// </summary>
        /// <param name="msecTimeout">The msec timeout.</param>
        /// <returns></returns>
        public RMQReceiverResultByte Receive(int msecTimeout = Timeout.Infinite)
        {
            var deliveryArgs = _Receive(msecTimeout);
            return new RMQReceiverResultByte(_connection, _channel, deliveryArgs, _queueSettings.QueueName);
        }
    }
}
