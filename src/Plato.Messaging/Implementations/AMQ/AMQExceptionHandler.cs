﻿// Plato.NET
// Copyright (c) 2016 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Plato.Messaging.Enums;
using Plato.Messaging.Exceptions;
using System;

namespace Plato.Messaging.Implementations.AMQ
{
    /// <summary>
    /// 
    /// </summary>
    internal static class AMQExceptionHandler
    {
        /// <summary>
        /// Exceptions the handler.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static Exception ExceptionHandler(IConnection connection, Exception ex)
        {
            if (ex is NullReferenceException)
            {
                if (connection == null)
                {
                    return new MessageException(MessageExceptionCode.LostConnection, ex.Message, ex);
                }
            }

            if (ex is System.IO.IOException 
            || ex is NMSConnectionException
            || ex is ConnectionClosedException
            || ex is BrokerException
            || ex is IOException
            || ex is InvalidClientIDException
            || ex is NMSException)
            {
                return new MessageException(MessageExceptionCode.LostConnection, ex.Message, ex);
            }

            return null;
        }
    }
}
