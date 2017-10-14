// Plato.NET
// Copyright (c) 2017 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.RMQ.Interfaces;
using Plato.Messaging.RMQ.Settings;
using System.Threading;

namespace Plato.Messaging.RMQ
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Plato.Messaging.RMQ.RMQConsumer" />
    /// <seealso cref="Plato.Messaging.RMQ.Interfaces.IRMQConsumerText" />
    public class RMQConsumerText : RMQConsumer, IRMQConsumerText
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RMQConsumerText" /> class.
        /// </summary>
        /// <param name="connectionFactory">The connection factory.</param>
        /// <param name="connectionSettings">The connection settings.</param>
        /// <param name="queueSettings">The queue settings.</param>
        public RMQConsumerText(
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
        public RMQReceiverResultText Receive(int msecTimeout = Timeout.Infinite)
        {
            var deliveryArgs = _Receive(msecTimeout);
            return deliveryArgs != null ? new RMQReceiverResultText(_connection, _channel, deliveryArgs, _queueSettings.QueueName) : null;
        }
    }
}
