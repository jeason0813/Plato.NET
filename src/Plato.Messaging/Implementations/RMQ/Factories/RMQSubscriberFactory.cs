﻿// Plato.NET
// Copyright (c) 2016 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.Implementations.RMQ.Interfaces;
using Plato.Messaging.Implementations.RMQ.Settings;

namespace Plato.Messaging.Implementations.RMQ.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class RMQSubscriberFactory : IRMQSubscriberFactory
    {
        private readonly IRMQConnectionFactory _connectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RMQProducerFactory"/> class.
        /// </summary>
        /// <param name="connectionFactory">The connection factory.</param>
        public RMQSubscriberFactory(IRMQConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IRMQSubscriberByte CreateByte(
            string connectionName, 
            RMQExchangeSettings exchangeSettings, 
            RMQQueueSettings queueSettings)
        {
            return new RMQSubscriberByte(_connectionFactory, connectionName, exchangeSettings, queueSettings);
        }

        public IRMQSubscriberText CreateText(
            string connectionName,
            RMQExchangeSettings exchangeSettings,
            RMQQueueSettings queueSettings)
        {
            return new RMQSubscriberText(_connectionFactory, connectionName, exchangeSettings, queueSettings);
        }
    }
}