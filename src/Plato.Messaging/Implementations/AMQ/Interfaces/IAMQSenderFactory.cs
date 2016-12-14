﻿// Plato.NET
// Copyright (c) 2016 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.Implementations.AMQ.Settings;

namespace Plato.Messaging.Implementations.AMQ.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAMQSenderFactory
    {
        /// <summary>
        /// Creates the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="connectionName">Name of the connection.</param>
        /// <returns></returns>
        IAMQSender Create(AMQDestinationSettings settings, string connectionName);

        /// <summary>
        /// Creates the text.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="connectionName">Name of the connection.</param>
        /// <returns></returns>
        IAMQSenderText CreateText(AMQDestinationSettings settings, string connectionName);

        /// <summary>
        /// Creates the bytes.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="connectionName">Name of the connection.</param>
        /// <returns></returns>
        IAMQSenderBytes CreateBytes(AMQDestinationSettings settings, string connectionName);
    }
}
